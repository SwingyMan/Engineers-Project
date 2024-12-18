import { useInfiniteQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import {
  createPost,
  fetchPosts,
} from "../services/posts.service";

export const usePosts = () => {
  const QueryKey = ["post"];
  
  const queryClient = useQueryClient();
  const PostQuery = () => {
    return useInfiniteQuery({
      queryKey: QueryKey,
      queryFn: fetchPosts,
      initialPageParam: 0,
      staleTime:60*1000,
      getNextPageParam: (lastPage, pages,lastPageParam) => {
        if(lastPage.length===0){
          return undefined
        }
      return lastPageParam+1
      
      },
    })
  };
  const handleAddPost = useMutation({
    mutationFn: async (newPost: {}) => {
      await createPost(newPost);
    },
    onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey }),
    //onError
  });
  const {data, error, isError, isFetched,hasNextPage,fetchNextPage,  isPending } = PostQuery();

  return {
    data,
    error,
    hasNextPage,
    fetchNextPage,
    isError,
    handleAddPost,
    isFetched,
    isPending,
  };
};
