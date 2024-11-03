import { del, get, patch, post } from "../API"
const url ='/posts'
export const fetchPosts=()/*:Promise<Post[]>*/ =>{
    return get(url)
}
export const createPost=(Post:any /*:Partial<Post>*/)/*:Promise<Post>*/=>{
    return post(url,Post)
}
export const editPost = (Post :any/*:Partial<Post>*/)/*:Promise<Post>*/=>{
    return patch(url,Post)
}
export const deletePost = (id:string):Promise<string>=>{
    return del(url+`/${id}`)
}