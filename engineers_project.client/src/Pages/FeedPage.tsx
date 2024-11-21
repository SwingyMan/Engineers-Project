import styled from "styled-components";
import { Post } from "../components/Post/Post";

import { ChatBox } from "../components/RightNavBar/ChatBox";
import { ChatFeed } from "../components/RightNavBar/ChatFeed";
import { usePosts } from "../API/hooks/usePosts";

const post:PostDTO = {
    title:"",
    body:"Lorem ipsum dolor, sit amet consectetur adipisicing elit. Consectetur voluptate nostrum dolores quidem distinctio placeat, laboriosam, sed fugit eum expedita, sapiente repellendus enim. Maxime iure possimus repellendus tempora eum recusandae!",
    user:{
        id:"1",
        username:"John Doe",
        avatarFileName:"src/assets/john-doe.jpg",
    },

    createdAt:new Date(Date.now()),
    id:"1",


};
//POSTY nowy komponent?

const PostFeed = styled.div`
flex:1;
  overflow-y: scroll;

  /* hide elements */
  /* visibility: hidden; */
  `

const obj:{} = {entity:{title: "string",body: "string",availability: 0}}

export function FeedPage() {
    const {data,isPending,isFetched,handleAddPost} =usePosts()

    if(isPending){
        console.log(data)
    }
    if(isFetched){
        console.log(data)
    }

    return (<>
            {/* <button onClick={()=>{handleAddPost.mutate(obj)}}> add</button> */}
            <PostFeed>
                {data&&data.map(post=><Post key={post.id} postInfo={post}/>)}

    
            </PostFeed>
            <ChatFeed>
            <ChatBox ChatName="a" Sender="JohnDoe" ChatImg="src/assets/john-doe.jpg" Message="QWERTY" ActivityDate={ Date.now()}/>
            <ChatBox ChatName="a" Sender="JohnDoe" ChatImg="src/assets/john-doe.jpg" Message="QWERTY" ActivityDate={ Date.now()}/>
            </ChatFeed>
</>
    )
}