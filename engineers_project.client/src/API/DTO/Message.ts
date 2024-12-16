import { User } from "./User";

export interface Message{
    Id:string;
    Content:string;
    CreationDate:string;
    user:User
}