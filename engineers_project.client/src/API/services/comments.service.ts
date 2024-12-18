import { del, get, patch, post } from "../API"
const url ='Comment/'
export const fetchComments=(postId:string):Promise<CommentDTO[]> =>{
    return get(url+'GetComments/'+postId)
}
export const createComment=(Comment:Partial<CommentDTO> ):Promise<CommentDTO>=>{
    return post(url+'AddComment',Comment)
}
export const editComment = (Comment :Partial<CommentDTO>):Promise<CommentDTO>=>{
    return patch(url+'UpdateComment/',Comment)
}
export const deleteComment= (id:string):Promise<string>=>{
    return del(url+`DeleteComment/${id}`)
}
