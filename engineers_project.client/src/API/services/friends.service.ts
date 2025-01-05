import { del, get, postEmpty } from "../API"
const url = 'Friend/'
export interface Friend {
    id: string
    userId1: string
    userId2: string
    accepted: boolean

}
export const fetchFriends = (): Promise<Friend[]> => {
    return get(url + 'getFriends')
}
export const sendFriendRequest = (friendId: string): Promise<Friend> => {
    return postEmpty(url + 'AddFriend?friendId=' + friendId)
}
export const acceptFriendRequest = (friendId: string): Promise<Friend> => {
    return postEmpty(url + 'AcceptFriend?friendId=' + friendId)
}
export const removeFriend = (id: string): Promise<string> => {
    return del(url + `RemoveFriend?friendId=${id}`)
}
