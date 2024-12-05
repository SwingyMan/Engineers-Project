import { useQuery } from "@tanstack/react-query";
import { fetchPostByTitle } from "../services/searchByName.service";

export const useSearchPosts = (query: string) => {
  const QueryKey = [query, "PostResult"];

  const SearchPostQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => fetchPostByTitle(QueryKey[0]),
      
      staleTime: 60 * 1000,
      initialDataUpdatedAt: 1000,
      enabled:query.length>=2
    });
  };
  const {data,isError, isFetching,isPending, error} = SearchPostQuery();
  return {data,isError, isFetching,isPending, error}
};
