import styled from "styled-components";
import { Post } from "../components/Post/Post";

import { ChatBox } from "../components/RightNavBar/ChatBox";
import { ChatFeed } from "../components/RightNavBar/ChatFeed";
import { usePosts } from "../API/hooks/usePosts";
import { CreatePost } from "../components/Post/CreatePost";
import { Comment } from "../components/Post/Comment";

const PostFeed = styled.div`
  flex: 1;
  overflow-y: scroll;
  color: var(--white);

`;
const comm :CommentDTO= {
 content:"ahsdb voiqjbdvuahbf vkj,ah d jhalhfd lnalhv alndz vlad nfmxckjvhn jklsfn v,mxńćvjha dcm,n kjhajldfh lamdn c,mxn ljha ncvm andc,jmh n,mvn slf hnamnv ,m ,fja; dfn,mvnz",
 avararName:"src/assets/john-doe.jpg",
 username:"qefqev vqeverv",
 postId:"",
 createdDate:new Date(Date.now()),
 userId:""
}
export function FeedPage() {
  const { data, isPending, isFetched } = usePosts();


  return (
    <>
      <PostFeed>
        <CreatePost />
        {isPending?"Loading...":(data &&
          data.pages.map((group, i) =>
            group.map((post) => <Post key={post.id} postInfo={post} details={0} />)
          ))}
          <Comment comment={comm}/>
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
