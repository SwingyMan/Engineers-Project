import { useQuery } from "@tanstack/react-query";
import { fetchGroupByName } from "../services/searchByName.service";

export const useSearchGroups = (query: string) => {
  const QueryKey = [query, "GroupResult"];

  const SearchGroupQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => fetchGroupByName(QueryKey[0]),
      
      staleTime: 60 * 1000,
      initialDataUpdatedAt: 1000,
      enabled:query.length>=2
    });
  };
  const {data,isError, isFetching,isPending, error} = SearchGroupQuery();
  return {data,isError, isFetching,isPending, error}
};
