import {
  useMutation,
  useQuery,
  useQueryClient,
} from "@tanstack/react-query";
import { getGroupMembership, requestGroupAccess } from "../services/groups.service";

export const useMyGroups = () => {
  const QueryKey = ["MyGroups"];
  const queryClient = useQueryClient();
  const SearchGroupQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => getGroupMembership(),

      staleTime: 60 * 1000,
    });
  };
  const requestToGroup = useMutation({
    mutationFn: async (id: string) => {
      return await requestGroupAccess(id);
    },
    onSuccess: (res) =>{ queryClient.invalidateQueries({ queryKey: QueryKey }),queryClient.invalidateQueries({queryKey:[res.GroupId]})},
  });
  const { data, isError, isFetching, isPending, error } = SearchGroupQuery();
  return { data, isError, isFetching, isPending, error, requestToGroup };
};
