import { del, get, patch, post } from "../API"
const url ='Post/'
export const fetchPosts=():Promise<PostDTO[]> =>{
    return get(url+'GetAvailablePosts')
}
export const createPost=(Post :Partial<PostDTO>):Promise<PostDTO>=>{
    return post(url+'Post',Post)
}
export const editPost = (Post :Partial<PostDTO>):Promise<PostDTO>=>{
    return patch(url+'Put/',Post)
}
export const deletePost = (id:string):Promise<string>=>{
    return del(url+`Delete/${id}`)
}