import styled from "styled-components";
import { Post } from "../components/Post/Post";

import { ChatBox } from "../components/RightNavBar/ChatBox";
import { ChatFeed } from "../components/RightNavBar/ChatFeed";
import { usePosts } from "../API/hooks/usePosts";
import { CreatePost } from "../components/Post/CreatePost";
import { useState } from "react";
import { useAuth } from "../Router/AuthProvider";

const PostFeed = styled.div`
  flex: 1;
  overflow-y: scroll;
  color: var(--white);

`;

export function FeedPage() {
  const { data, isPending, isFetched } = usePosts();
  const [openMenu,setOpenMenu] = useState<null|string>(null)
  const handleMenuOpen=(id:string)=>{
    console.log(id)
    setOpenMenu(id)
  }
  const {user} = useAuth()
  return (
    <>
      <PostFeed>
        <CreatePost />
        {isPending?"Loading...":(data &&
          data.pages.map((group, i) =>
            group.map((post) => <Post key={post.id} postInfo={post} isMenu={post.userId===user?.id} isOpen={openMenu===post.id} setIsOpen={()=>handleMenuOpen(post.id)}/>)
          ))}
      </PostFeed>
      <ChatFeed>
        <ChatBox
          ChatName="a"
          Sender="JohnDoe"
          ChatImg="src/assets/john-doe.jpg"
          Message="QWERTY"
          ActivityDate={Date.now()}
        />
        <ChatBox
          ChatName="a"
          Sender="JohnDoe"
          ChatImg="src/assets/john-doe.jpg"
          Message="QWERTY"
          ActivityDate={Date.now()}
        />
      </ChatFeed>
    </>
  );
}
