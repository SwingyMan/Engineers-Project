import { useQuery } from "@tanstack/react-query";
import {  fetchUserByName } from "../services/searchByName.service";

export const useSearchUsers = (query: string) => {
  const QueryKey = [query, "UserResult"];
  const SearchPostQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => fetchUserByName(QueryKey[0]),
      
      staleTime: 60 * 1000,
      initialDataUpdatedAt: 1000,
      enabled:query.length>=2
    });
  };
  const {data,isError, isFetching,isPending, error} = SearchPostQuery();
  return {data,isError, isFetching,isPending, error}
};
