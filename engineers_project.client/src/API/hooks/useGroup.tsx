import {
  useQueryClient,
  useQuery,
  useMutation,
} from "@tanstack/react-query";
import { editGroup, deleteGroup, getGroupById, acceptGroupAccess, deleteFromGroup } from "../services/groups.service";
import { EditGroup, GroupDTO } from "../DTO/GroupDTO";

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
    mutationFn: async (editedGroup:EditGroup) => {
      await editGroup(editedGroup);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: QueryKey });
    },
  });
  const handleDeleteGroup = useMutation({
    mutationFn: async () => {
      await deleteGroup(id);
    },
    onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey }),
  });
  const handleAddToGroup = useMutation({
    mutationFn: async (userId:string)=>{
      return acceptGroupAccess(id,userId)
    },
    onSuccess:()=>{queryClient.invalidateQueries({queryKey:QueryKey})}
  })
  const handleDeleteFromGroup  = useMutation({
    mutationFn:async (userId:string)=>{ deleteFromGroup(id,userId)},
    onSuccess:()=>{queryClient.invalidateQueries({queryKey:QueryKey})}
  })

  return {
    data,
    handleEditGroup,
    handleDeleteGroup,
    error,
    isError,
    isFetched,
    isPending,
    handleAddToGroup,
    handleDeleteFromGroup
  };
};
