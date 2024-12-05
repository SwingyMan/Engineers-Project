import { get } from "../API"
import { PostDTO } from "../DTO/PostDTO"
import { User } from "../DTO/User"
const url1 ='Post/FindPostByTitle?title='
const url2 ='User/GetUserByName?userName='
export const fetchPostByTitle = (query:string):Promise<PostDTO[]>=>{
    return get(url1+query)
}
export const fetchUserByName = (query:string):Promise<User[]>=>{
    return get(url2+query)
}