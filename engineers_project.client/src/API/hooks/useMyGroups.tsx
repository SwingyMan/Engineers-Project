import { useQuery } from "@tanstack/react-query";
import { getGroupMembership } from "../services/groups.service";

export const useMyGroups = () => {
  const QueryKey = ["MyGroups"];

  const SearchGroupQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => getGroupMembership(),
      
      staleTime: 60 * 1000,

    });
  };
  const {data,isError, isFetching,isPending, error} = SearchGroupQuery();
  return {data,isError, isFetching,isPending, error}
};