import { del, get, patch, post } from "../API"
const url ='Group/'
export const getAllGroups=():Promise<GroupDTO[]>=>{
    return get(url+'GetAll')
}

export const createGroup=(Group:Partial<GroupDTO> ):Promise<GroupDTO>=>{
    return post(url+'Post',Group)
}
export const editGroup = (Group :Partial<GroupDTO>):Promise<GroupDTO>=>{
    return patch(url+'Patch/'+Group.id,Group)
}
export const deleteGroup= (id:string):Promise<string>=>{
    return del(url+`Delete/${id}`)
}
export const getGroupById=(id:string):Promise<GroupDTO>=>{
    return get(url+'Get/'+id)
}
export const requestGroupAccess = (id:string)=>{
    return get(url+'RequestToGroup?groupId='+id)
}
export const acceptGroupAccess = (id:string,userId:string)=>{
    return get(url+'AcceptToGroup?groupId='+id+'?userId='+userId)
}