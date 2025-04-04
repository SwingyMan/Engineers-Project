import { User } from "./User"

export interface GroupDTO {
    id: string,
    name: string,
    description: string,
    groupUsers: {
        isAccepted: boolean,
        isOwner: boolean,
        user:User
    }[]

}
export interface EditGroup{
    groupId:string,
    groupName:string,
    groupDescription:string,
}