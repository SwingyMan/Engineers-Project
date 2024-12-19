import {
  useQueryClient,
  useQuery,
  useMutation,
  InfiniteData,
} from "@tanstack/react-query";
import { editPost, deletePost, fetchPost } from "../services/posts.service";
import { PostDTO } from "../DTO/PostDTO";

export const usePostDetails = (id: string) => {
  const QueryKey = [id];
  const queryClient = useQueryClient();
  const init: InfiniteData<PostDTO[], unknown> | undefined =
    queryClient.getQueryData(["post"]);

  const PostQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => fetchPost(QueryKey[0]),
      initialData: () => init?.pages[0].find((d) => d.id === QueryKey[0]),
      staleTime: 60 * 1000,
      initialDataUpdatedAt: 1000,
    });
  };
  const { isError, isFetched, data, error, isPending } = PostQuery();

  const handleEditPost = useMutation({
    mutationFn: async (editedPost: Partial<PostDTO>) => {
      return await editPost(editedPost);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: QueryKey })
    },
  });
  const handleDeletePost = useMutation({
    mutationFn: async (id: string) => {
     return  await deletePost(id);
    },
    onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey }),
  });

  return {
    data,
    handleEditPost,
    handleDeletePost,
    error,
    isError,
    isFetched,
    isPending,
  };
};
