export class Post{
    content: string
    name: string
    profileLink:string
    dateOfCreation: number
    img: string
    commentCount: number
    constructor(content:string,
        name: string,
        profileLink:string,
        dateOfCreation:number,
        img:string,
        commentCount:number){
        this.content=content
        this.name=name
        this.profileLink=profileLink
        this.dateOfCreation=dateOfCreation
        this.img=img
        this.commentCount= commentCount
      
    }
}