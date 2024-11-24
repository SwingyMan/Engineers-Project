import { del, get, patch, post } from "../API"
const url ='Post/'
const url1 ='Comment/'
export const fetchComments=(postId:string):Promise<CommentDTO[]> =>{
    return get(url+'GetComments/'+postId)
}
export const createComment=(Comment:Partial<CommentDTO> ):Promise<CommentDTO>=>{
    return post(url1+'Post',Comment)
}
export const editComment = (Comment :Partial<CommentDTO>):Promise<CommentDTO>=>{
    return patch(url1+'Put/',Comment)
}
export const deleteComment= (id:string):Promise<string>=>{
    return del(url1+`Delete/${id}`)
}
