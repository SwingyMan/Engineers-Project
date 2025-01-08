import { useQuery } from "@tanstack/react-query";
import { getAllGroups } from "../services/groups.service";

export const useGroups = () => {
  const QueryKey = ["Groups"];

  const GroupQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => getAllGroups(),
      staleTime: 60 * 1000,

    });
  };
  const {data,isError, isFetched,isPending, error} = GroupQuery();
  return {data,isError, isFetched,isPending, error}
};
