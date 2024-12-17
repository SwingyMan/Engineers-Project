import {
    useQueryClient,
    useQuery,
    useMutation,
  } from "@tanstack/react-query";
  import { editPost, deletePost } from "../services/posts.service";
  import { PostDTO } from "../DTO/PostDTO";
import { fetchPostsInGroup } from "../services/posts.service";
  
  export const useGroupPosts = (id: string, member:boolean) => {
    const QueryKey = [id,"GroupPosts"];
    const queryClient = useQueryClient();

    const GroupPostQuery = () => {
      return useQuery({
        queryKey: QueryKey,
        queryFn: () => fetchPostsInGroup(QueryKey[0]),
        staleTime: 60 * 1000,
        enabled:member
      });
    };
    const { isError, isFetched, data, error, isPending } = GroupPostQuery();
  
    const handleEditGroupPost = useMutation({
      mutationFn: async (editedGroupPost: Partial<PostDTO>) => {
        await editPost(editedGroupPost);
      },
      onSuccess: () => {
        queryClient.invalidateQueries({ queryKey: QueryKey })
      },
    });
    const handleDeleteGroupPost = useMutation({
      mutationFn: async (id: string) => {
        await deletePost(id);
      },
      onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey }),
    });
  
    return {
      data,
      handleEditGroupPost,
      handleDeleteGroupPost,
      error,
      isError,
      isFetched,
      isPending,
    };
  };
  