import { UUID } from "crypto";


export interface PostDTO{
    id:UUID,
    title: string,
    body: string,
    status?: string,
    availability?: 1|0,
    createdAt: Date,
    attachments?: string|null,
    comments:CommentDTO[],
    userId:UUID,
    username:string,
    avatarName:string
}