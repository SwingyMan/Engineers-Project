import { del, get, patch, post } from "../API"
const url ='Post/'
export const fetchPosts=({pageParam}:{pageParam:number}):Promise<PostDTO[]> =>{
    return get(url+'GetAvailablePosts')
}
export const fetchPost = (id:string):Promise<PostDTO>=>{
    return get(url+'Get/'+id)
}
export const createPost=(Post:{} ):Promise<PostDTO>=>{
    return post(url+'Post',Post)
}
export const editPost = (Post :Partial<PostDTO>):Promise<PostDTO>=>{
    return patch(url+'Put/',Post)
}
export const deletePost = (id:string):Promise<string>=>{
    return del(url+`Delete/${id}`)
}