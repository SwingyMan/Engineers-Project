import { del, get, patch, post } from "../API"
import { PostDTO } from "../DTO/PostDTO"
import { User } from "../DTO/User"
import { UserDTO } from "../DTO/UserDTO"
const url = "User/" 
export const fetchUserById=(id:string):Promise<User>=>{
    return get(url+'GetById?guid='+id)
}
export const editUserById = (userInfo:{})=>{
    return patch(url+"UpdateByID",userInfo)
}
export const fetchPostsByUser=(id:string):Promise<PostDTO[]>=>{
    return get("Post/FindPostByUser?userId="+id)
}
export const deleteUserById=(id:string)=>{
    return del(url+"/DeleteByID?guid="+id)
}
export const register = (userData:UserDTO)=>{
    return post(url+"Register",userData)
}