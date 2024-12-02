import styled from "styled-components";
import { Post } from "../components/Post/Post";

import { ChatBox } from "../components/RightNavBar/ChatBox";
import { ChatFeed } from "../components/RightNavBar/ChatFeed";
import { usePosts } from "../API/hooks/usePosts";
import { CreatePost } from "../components/Post/CreatePost";

const PostFeed = styled.div`
  flex: 1;
  overflow-y: scroll;
  color: var(--white);

`;

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
