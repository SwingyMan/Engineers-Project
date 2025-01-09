import { del, get, patch, post, postEmpty } from "../API"
import { Group } from "../DTO/Group"
import { EditGroup, GroupDTO } from "../DTO/GroupDTO"
import { GroupUser } from "../DTO/GroupUser"
const url = 'Group/'

export const getGroupById = (id: string): Promise<GroupDTO> => {
    return get(url + 'Get/' + id)
}

export const createGroup = (Group: Partial<GroupDTO>): Promise<GroupDTO> => {
    return post(url + 'Post', Group)
}

export const editGroup = (Group: EditGroup): Promise<GroupDTO> => {
    return patch(url + 'Patch', Group)
}

export const deleteGroup = (id: string): Promise<string> => {
    return del(url + `Delete/${id}`)
}

export const requestGroupAccess = (id: string) :Promise<GroupUser>=> {
    return get(url + 'RequestToGroup?groupId=' + id)
}

export const acceptGroupAccess = (id: string, userId: string) :Promise<GroupUser>=> {
    return get(url + 'AcceptToGroup?groupId=' + id + '?userId=' + userId)
}

export const deleteFromGroup = (groupId:string, userId: string):Promise<null>=>{
    return del(url + `DeleteFromGroup?groupId=${groupId}&userId=${userId}`)
}
export const getGroupMembership=():Promise<Group[]>=>{
    return get(url+`GetGroupMembership`)
}
export const getAllGroups = (): Promise<GroupDTO[]> => {
    return postEmpty(url + 'GetAll')
}
export const getGroupRequests  =():Promise<Group[]>=>{
    return get(url +`GetGroupRequests`)
}

export const getRequestsToGroup  =():Promise<Group[]>=>{
    return get(url +`GetRequestsToGroup`)
}