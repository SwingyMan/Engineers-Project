import { get } from "../API"
import { Group } from "../DTO/Group"
import { PostDTO } from "../DTO/PostDTO"
import { User } from "../DTO/User"
const url1 ='Post/FindPostByTitle?title='
const url2 ='User/GetUserByName?userName='
const url3 ='Group/GetGroupByName?name='
export const fetchPostByTitle = (query:string):Promise<PostDTO[]>=>{
    return get(url1+query)
}
export const fetchUserByName = (query:string):Promise<User[]>=>{
    return get(url2+query)
}
export const fetchGroupByName = (query:string):Promise<Group[]>=>{
    return get(url3+query)
}