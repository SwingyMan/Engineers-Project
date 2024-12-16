import { del, get, patch, post } from "../API"
import { Group } from "../DTO/Group"
import { GroupDTO } from "../DTO/GroupDTO"
const url = 'Group/'

export const getGroupById = (id: string): Promise<GroupDTO> => {
    return get(url + 'Get/' + id)
}

export const createGroup = (Group: Partial<GroupDTO>): Promise<GroupDTO> => {
    return post(url + 'Post', Group)
}

export const editGroup = (Group: Partial<GroupDTO>): Promise<GroupDTO> => {
    return patch(url + 'Patch/' + Group.id, Group)
}

export const deleteGroup = (id: string): Promise<string> => {
    return del(url + `Delete/${id}`)
}

export const requestGroupAccess = (id: string) => {
    return get(url + 'RequestToGroup?groupId=' + id)
}

export const acceptGroupAccess = (id: string, userId: string) => {
    return get(url + 'AcceptToGroup?groupId=' + id + '?userId=' + userId)
}

export const delertFromGroup = (groupId:string, userId: string)=>{
    return del(url + `DeleteFromGroup?groupId=${groupId}&userId=${userId}`)
}
export const getGroupMembership=():Promise<Group[]>=>{
    return get(url+`GetGroupMembership`)
}
export const getAllGroups = (): Promise<GroupDTO[]> => {
    return get(url + 'GetAll')
}
export const getGroupRequests  =():Promise<Group[]>=>{
    return get(url +`GetGroupRequests`)
}

