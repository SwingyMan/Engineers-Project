import { useQuery, useQueryClient } from "@tanstack/react-query";
import { fetchPostsByUser } from "../services/user.service";

export const useUserPosts = (id:string)=>{
    const QueryKey = [id, "UserPosts"];
    const queryClient = useQueryClient();
    const SearchPostQuery = () => {
      return useQuery({
        queryKey: QueryKey,
        queryFn: () => fetchPostsByUser(QueryKey[0]),
        staleTime: 60 * 1000,
      });
    };
    
  const { data, isError, isFetching, isPending, error } = SearchPostQuery();
  return { data, isError, isFetching, isPending, error }
}