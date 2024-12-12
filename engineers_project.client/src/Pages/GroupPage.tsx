import { useParams } from "react-router"

export function GroupPage(){
    const {id } = useParams()
    return(<><h1 color="white"> {id}</h1></>)
}