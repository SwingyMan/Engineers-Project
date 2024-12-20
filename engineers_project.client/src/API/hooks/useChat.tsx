import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { getChat, sendMessage} from "../services/chat.service"
import { ChatMessage } from "../DTO/ChatMessage";

export const useChat = (id:string) => {
  const QueryKey = [id,"Chat"];
 const queryClient = useQueryClient()
  const SearchChatQuery = () => {
    return useQuery({
      queryKey: QueryKey,
      queryFn: () => getChat(id),
      
      staleTime: 60 * 1000,

    });
  };
  const handleSendMessage = useMutation({
    mutationFn: async (message:ChatMessage)=>{
      return sendMessage(message)
    },
    onSuccess:()=>{queryClient.invalidateQueries({queryKey:QueryKey})},
    onError:(e)=>{alert(e)}
  })
  const handleReciveMessage = useMutation({})
  const {data,isError, isFetching,isPending, error} = SearchChatQuery();
  return {data,isError, isFetching,isPending, error,handleSendMessage}
};
