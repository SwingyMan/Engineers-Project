import { postAttachment } from "../API"


const url = "Attachment/"
export const postAttachments = (data:FormData) => {
    return postAttachment(url + `Post`,data)
}