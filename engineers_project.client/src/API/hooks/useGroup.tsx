import {
  useQueryClient,
  useQuery,
  useMutation,
} from "@tanstack/react-query";
import { editGroup, deleteGroup, getGroupById } from "../services/groups.service";
import { GroupDTO } from "../DTO/GroupDTO";

export const useGroupDetails = (id: string) => {
  const QueryKey = [id];
  const queryClient = useQueryClient();
  const init: GroupDTO[] | undefined =
    queryClient.getQueryData(["group"]);

  const GroupQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => getGroupById(QueryKey[0]),
      initialData: () => init?.find((d) => d.id === QueryKey[0]),
      staleTime: 60 * 1000,
      initialDataUpdatedAt: 1000,
    });
  };
  const { isError, isFetched, data, error, isPending } = GroupQuery();

  const handleEditGroup = useMutation({
    mutationFn: async (editedGroup: Partial<GroupDTO>) => {
      await editGroup(editedGroup);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: QueryKey });
    },
  });
  const handleDeleteGroup = useMutation({
    mutationFn: async (id: string) => {
      await deleteGroup(id);
    },
    onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey }),
  });

  return {
    data,
    handleEditGroup,
    handleDeleteGroup,
    error,
    isError,
    isFetched,
    isPending,
  };
};
