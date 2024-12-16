import { UUID } from "crypto";


export interface PostDTO{
    id:UUID,
    title: string,
    body: string,
    status?: string,
    availability?: 1|0,
    createdAt: Date,
    attachments?: AttachmentDTO[]|null,
    comments:CommentDTO[],
    userId:UUID,
    groupId?:string,
    username:string,
    avatarName:string
}