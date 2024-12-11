import { del, get, patch, post } from "../API"
const url ='Group/'
export const getAllGroups=():Promise<GroupDTO[]>=>{
    return get(url+'GetAll')
}

export const createGroup=(Group:Partial<GroupDTO> ):Promise<GroupDTO>=>{
    return post(url+'Post',Group)
}
export const editGroup = (Group :Partial<GroupDTO>):Promise<GroupDTO>=>{
    return patch(url+'UpdateGroup/',Group)
}
export const deleteGroup= (id:string):Promise<string>=>{
    return del(url+`DeleteGroup/${id}`)
}
