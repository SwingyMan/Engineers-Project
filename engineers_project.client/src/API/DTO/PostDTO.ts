export interface PostDTO{
    id:string,
    title: string,
    body: string,
    status?: string,
    availability?: 1|0,
    createdAt: Date,
    attachments?: AttachmentDTO[]|null,
    comments:CommentDTO[],
    userId:string,
    groupId?:string,
    username:string,
    avatarName:string
}