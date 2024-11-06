import { del, get, patch, post } from "../API"
const url ='/posts'
export const fetchPosts=():Promise<PostDTO[]> =>{
    return get(url)
}
export const createPost=(Post :Partial<PostDTO>):Promise<PostDTO>=>{
    return post(url,Post)
}
export const editPost = (Post :Partial<PostDTO>):Promise<PostDTO>=>{
    return patch(url,Post)
}
export const deletePost = (id:string):Promise<string>=>{
    return del(url+`/${id}`)
}