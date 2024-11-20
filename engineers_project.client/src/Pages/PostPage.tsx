import { Post } from "../components/Post/Post";

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
export function PostPage(){
    return(
        <>
        <Post postInfo={post}/>
        </>
    )
}