import { useLocation } from "react-router";
import { usePostDetails } from "../API/hooks/usePostDetails";
import styled from "styled-components";
import { PostDetails } from "../components/Post/PostDetails";
import { useAuth } from "../Router/AuthProvider";
import { useState } from "react";

const PostWrapper = styled.div`
    flex: 1;
    overflow-y: scroll;
    display: flex;
    justify-content: center;
    color: var(--white);
`

export function PostPage(){
    const location=useLocation()
    const {user} = useAuth()
    const {data} = usePostDetails(location.pathname.slice(6))
    const [openMenu,setOpenMenu] = useState<null|string>(null)
    const handleMenuOpen=(id:string)=>{
      console.log(id)
      setOpenMenu(id)
    }
    return(
        <PostWrapper>
        {data&&<PostDetails postInfo={data} options={user?.id===data.userId} isOpen={openMenu===data.id} setIsOpen={()=>handleMenuOpen(data.id)} />}
        </PostWrapper>
    )
}