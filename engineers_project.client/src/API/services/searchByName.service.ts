import { get } from "../API"
import { UserDTO } from "../DTO/UserDTO"
const url1 ='Post/FindPostByTitle?title='
const url2 ='User/GetUserByName?userName='
export const fetchPostByTitle = (query:string):Promise<PostDTO[]>=>{
    console.log(query)
    return get(url1+query)
}
export const fetchUserByName = (query:string):Promise<UserDTO[]>=>{
    console.log(query)
    return get(url2+query)
}