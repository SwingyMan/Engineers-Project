import { useLocation } from "react-router";
import { Post } from "../components/Post/Post";
import { usePostDetails } from "../API/hooks/usePostDetails";


export function PostPage(){
    const location=useLocation()
    const {data} = usePostDetails(location.pathname.slice(6))
    return(
        <>
        {data&&<Post postInfo={data}/>}
        </>
    )
}