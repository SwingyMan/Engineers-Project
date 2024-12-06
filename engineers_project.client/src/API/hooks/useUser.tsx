import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { fetchUserById, editUserById, deleteUserById } from "../services/user.service";

export const useUsers = (id: string) => {
  const QueryKey = [id, "User"];
  const queryClient = useQueryClient();
  const SearchPostQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => fetchUserById(QueryKey[0]),
      staleTime: 60 * 1000,
    });
  };
  const { data, isError, isFetching, isPending, error } = SearchPostQuery();
  const handleEditUser = useMutation({
    mutationFn: async (editUser: {}) => {
      await editUserById(editUser);
    },
    onSuccess: () => queryClient.invalidateQueries({ queryKey: QueryKey }),
  });
  const handleDeleteUser = useMutation({
    mutationFn: async (userId: string) => {
      await deleteUserById(userId);
    },
    onSuccess:()=>{
      //kaine idee
    }
  });
  return {
    data,
    isError,
    isFetching,
    isPending,
    error,
    handleEditUser,
    handleDeleteUser,
  };
};
