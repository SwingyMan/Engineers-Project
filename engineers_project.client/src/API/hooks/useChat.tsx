import { useQuery } from "@tanstack/react-query";
import { getChat} from "../services/chat.sercice"

export const useChat = (id:string) => {
  const QueryKey = [id,"Chat"];

  const SearchChatQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => getChat(id),
      
      staleTime: 60 * 1000,

    });
  };
  const {data,isError, isFetching,isPending, error} = SearchChatQuery();
  return {data,isError, isFetching,isPending, error}
};
