import { get, post } from "../API"
import { Chat } from "../DTO/Chat"
import { ChatMessage } from "../DTO/ChatMessage"
import { Message } from "../DTO/Message"

const url = "Chat/"

export const sendMessage=(Message:ChatMessage):Promise<Message>=>{
    return post(url+'SendMessage',Message)
}
export const getAll=():Promise<Chat[]>=>{
    return get(url+'GetAll')
}