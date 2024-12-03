import { get } from "../API"
import { UserDTO } from "../DTO/UserDTO"
const url1 ='Post/FindPostByTitle?title='
const url2 ='User/GetUserByName?userName='
export const fetchPostByTitle = (query:string):Promise<PostDTO[]>=>{
    return get(url1+query)
}
export const fetchUserByName = (query:string):Promise<UserDTO[]>=>{
    return get(url2+query)
}