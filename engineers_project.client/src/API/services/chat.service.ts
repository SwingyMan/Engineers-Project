import { get, post } from "../API"
import { Chat } from "../DTO/Chat"
import { ChatMessage } from "../DTO/ChatMessage"
import { Message } from "../DTO/Message"

const url = "Chat/"

export const sendMessage=(Message:ChatMessage):Promise<Message>=>{
    return post(url+'SendMessage',Message)
}
export const getAllUserChats=():Promise<Chat[]>=>{
    return get(url+'GetAllUserChats')
}
export const getOrCreateChat=(id:string):Promise<Chat>=>{
    return post(url+"GetOrCreateChat" , {recepientId:id})
}
export const getChat=(id:string):Promise<Chat>=>{
    return get(url+`Get/${id}` )
}