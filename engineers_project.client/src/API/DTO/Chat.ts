import { Message } from "./Message";
import { User } from "./User";

export interface Chat{
    id:string;
    name:string;
    messages:Message[],
    users:User[],
}