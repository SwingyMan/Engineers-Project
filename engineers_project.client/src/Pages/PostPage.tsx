import { useLocation } from "react-router";
import { Post } from "../components/Post/Post";
import { usePostDetails } from "../API/hooks/usePostDetails";
import styled from "styled-components";

const PostWrapper = styled.div`
    flex: 1;
    overflow-y: scroll;
    display: flex;
    justify-content: center;
`

export function PostPage(){
    const location=useLocation()
    const {data} = usePostDetails(location.pathname.slice(6))
    return(
        <PostWrapper>
        {data&&<Post postInfo={data} details={1}/>}
        </PostWrapper>
    )
}