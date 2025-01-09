import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import {
  acceptFriendRequest,
  fetchFriends,
  removeFriend,
  sendFriendRequest,
} from "../services/friends.service";

export const useFriends = () => {
  const QueryKey = ["Friends"];

  const queryClient = useQueryClient();
  const FriendsQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => fetchFriends(),
      staleTime: 60 * 1000,
    });
  };
  const handleRequestFriend = useMutation({
    mutationFn: async (friendId: string) => {
      await sendFriendRequest(friendId);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: QueryKey });
    },
  });
  const handlAcceptFriend = useMutation({
    mutationFn: async (friendId: string) => {
      await acceptFriendRequest(friendId);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: QueryKey });
    },
  });
  const handleRemoveFriend = useMutation({
    mutationFn: async (friendId: string) => {
      await removeFriend(friendId);
    },
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: QueryKey });
    },
  });

  const { data, isError, isFetching, isPending, error } = FriendsQuery();
  return {
    data,
    handleRequestFriend,
    handlAcceptFriend,
    handleRemoveFriend,
    isError,
    isFetching,
    isPending,
    error,
  };
};
