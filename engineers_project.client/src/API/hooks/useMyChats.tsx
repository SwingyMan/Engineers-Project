import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { getAllUserChats, getOrCreateChat } from "../services/chat.service";

export const useMyChats = () => {
  const QueryKey = ["MyChats"];
  const queryClient = useQueryClient();
  const SearchChatQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => getAllUserChats(),

      staleTime: 60 * 1000,
    });
  };
  const createNewChat = useMutation({
    mutationFn: async (id: string) => {
      return await getOrCreateChat(id);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: QueryKey });
    },
  });
  const { data, isError, isFetching, isPending, error } = SearchChatQuery();

  return { data, isError, createNewChat, isFetching, isPending, error };
};
