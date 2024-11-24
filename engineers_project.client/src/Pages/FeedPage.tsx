import styled from "styled-components";
import { Post } from "../components/Post/Post";

import { ChatBox } from "../components/RightNavBar/ChatBox";
import { ChatFeed } from "../components/RightNavBar/ChatFeed";
import { usePosts } from "../API/hooks/usePosts";
import { CreatePost } from "../components/Post/CreatePost";

const PostFeed = styled.div`
  flex: 1;
  overflow-y: scroll;
`;

export function FeedPage() {
  const { data, isPending, isFetched } = usePosts();


  return (
    <>
      <PostFeed>
        <CreatePost />
        {data &&
          data.pages.map((group, i) =>
            group.map((post) => <Post key={post.id} postInfo={post} />)
          )}
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
