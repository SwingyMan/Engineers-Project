import styled from "styled-components";
import { Post } from "../components/Post/Post";

import { ChatBox } from "../components/RightNavBar/ChatBox";
import { ChatFeed } from "../components/RightNavBar/ChatFeed";
import { Message } from "../components/Chat/Message";
import { usePosts } from "../API/hooks/usePosts";

const post:PostDTO = {
    title:"",
    body:"Lorem ipsum dolor, sit amet consectetur adipisicing elit. Consectetur voluptate nostrum dolores quidem distinctio placeat, laboriosam, sed fugit eum expedita, sapiente repellendus enim. Maxime iure possimus repellendus tempora eum recusandae!",
    user:{
        id:"",
        username:"John Doe",
        avatarFileName:"src/assets/john-doe.jpg",
    },

    createdAt:new Date(Date.now()),
    id:"",


};
//POSTY nowy komponent?

const PostFeed = styled.div`
flex:1;
  overflow-y: scroll;

  /* hide elements */
  /* visibility: hidden; */
  `
const StyledPage = styled.div`
    flex:1;
    display: flex;
    overflow: auto;
  `

export function FeedPage() {
    const {data,isPending,isFetched} =usePosts()

    if(isPending){
        console.log(data)
    }
    if(isFetched){
        console.log(data)
    }

    return (
        <StyledPage>
            <PostFeed>
                {data&&data.map(post=><Post key={post.id} postInfo={post}/>)}
                <Post
                    postInfo={post}
                />
                <Post postInfo={post} />
                <Post postInfo={post} /><Post postInfo={post} />
                <Post postInfo={post} /><Post postInfo={post} />
            
            </PostFeed>
            <ChatFeed>
            <ChatBox ChatName="a" Sender="JohnDoe" ChatImg="src/assets/john-doe.jpg" Message="QWERTY" ActivityDate={ Date.now()}/>
            <ChatBox ChatName="a" Sender="JohnDoe" ChatImg="src/assets/john-doe.jpg" Message="QWERTY" ActivityDate={ Date.now()}/>
            </ChatFeed>
        </StyledPage>
    )
}