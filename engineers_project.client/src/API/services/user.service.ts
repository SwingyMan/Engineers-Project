import { del, get, patch } from "../API"
import { PostDTO } from "../DTO/PostDTO"
import { User } from "../DTO/User"
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