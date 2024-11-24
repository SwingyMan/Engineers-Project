import { useQueryClient, useQuery, useMutation, InfiniteData } from "@tanstack/react-query";
import {
  createPost,
  editPost,
  deletePost,
  fetchPost,
} from "../services/posts.service";
export const usePostDetails = (id: string) => {
  const QueryKey = ["post", id];
  const queryClient = useQueryClient();
  const init: InfiniteData<PostDTO[], unknown> | undefined=queryClient.getQueryData(["post"])

  const PostQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => fetchPost(QueryKey[1]),
      initialData: () => init?.pages[0].find((d)=>d.id===QueryKey[1]),
      staleTime: 60 * 1000,
      initialDataUpdatedAt:1000
    });
  };
  const { isError, isFetched, data, error, isPending } = PostQuery();
  const handleAddPost = useMutation({
    mutationFn: async (newPost: {}) => {
      await createPost(newPost);
    },
    onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey }),
    //onError
  });
  const handleEditPost = useMutation({
    mutationFn: async (editedPost: Partial<PostDTO>) => {
      await editPost(editedPost);
    },
    onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey }),
  });
  const handleDeletePost = useMutation({
    mutationFn: async (id: string) => {
      await deletePost(id);
    },
    onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey }),
  });

  return {
    data,

    handleAddPost,
    handleEditPost,
    handleDeletePost,
    error,
    isError,
    isFetched,
    isPending,
  };
};
