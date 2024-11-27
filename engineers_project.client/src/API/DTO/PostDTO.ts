

interface PostDTO{
    id:string,
    title: string,
    body: string,
    status?: string,
    availability?: 1|0,
    createdAt: Date,
    attachments?: string|null,
    comments:[],
    userId:string,
    username:string,
    avatarFileName:string
}