import { get } from "../API"

const url = "Attachment/"
export const getFile = (id: string) => {
    return get(url + `GetFile/${id}`)
}