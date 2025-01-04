import { useQuery } from "@tanstack/react-query";
import { fetchGroupByName } from "../services/searchByName.service";

export const useGroups = () => {
  const QueryKey = ["Groups"];

  const GroupQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => fetchGroupByName(QueryKey[0]),
      staleTime: 60 * 1000,
      initialDataUpdatedAt: 1000,

    });
  };
  const {data,isError, isFetching,isPending, error} = GroupQuery();
  return {data,isError, isFetching,isPending, error}
};
