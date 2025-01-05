import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import {
  acceptFriendRequest,
  fetchFriends,
  Friend,
  removeFriend,
  sendFriendRequest,
} from "../services/friends.service";

export const useFriends = () => {
  const QueryKey = ["Friends"];

  const queryClient = useQueryClient();
  const GroupQuery = () => {
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
  const groupObjects = (data: Friend[], id: string) => {
    const id1Group: string[] = [];
    const id2Group: string[] = [];
    const acceptedGroup: string[] = [];

    data.forEach((obj) => {
      console.log(obj);
      if (obj.accepted) {
        acceptedGroup.push(obj.userId1===id?obj.userId2:obj.userId1);
      } else {
        if (obj.userId1 === id) {
          id1Group.push(obj.userId2);
        }
        if (obj.userId2 === id) {
          id2Group.push(obj.userId1);
        }
      }
    });

    return { send: id1Group, recived: id2Group, accepted: acceptedGroup };
  };
  const { data, isError, isFetching, isPending, error } = GroupQuery();
  return {
    data,
    handleRequestFriend,
    handlAcceptFriend,
    handleRemoveFriend,
    groupObjects,
    isError,
    isFetching,
    isPending,
    error,
  };
};
