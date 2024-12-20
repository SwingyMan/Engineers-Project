import { useQuery } from "@tanstack/react-query";
import {getAllUserChats} from "../services/chat.service"

export const useMyChats = () => {
  const QueryKey = ["MyChats"];

  const SearchChatQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => getAllUserChats(),
      
      staleTime: 60 * 1000,

    });
  };
  const {data,isError, isFetching,isPending, error} = SearchChatQuery();
  return {data,isError, isFetching,isPending, error}
};
