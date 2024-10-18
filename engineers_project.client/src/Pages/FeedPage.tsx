import styled from "styled-components";
import { MessageBox } from "../components/MessageBox/MessageBox";
import { Post } from "../components/MessageBox/PostClass";
import { ChatBox } from "../components/RightNavBar/ChatBox";
import { ChatFeed } from "../components/RightNavBar/ChatFeed";

let post = new Post(
    "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Consectetur voluptate nostrum dolores quidem distinctio placeat, laboriosam, sed fugit eum expedita, sapiente repellendus enim. Maxime iure possimus repellendus tempora eum recusandae!",
    "John Doe",
    "https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Classes",
    Date.now(),
    "src/assets/john-doe.jpg",
    2
);
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
    return (
        <StyledPage>
            <PostFeed>
                <MessageBox
                    postInfo={new Post("a", "a", "a", 1, "src/assets/john-doe.jpg", 1)}
                />
                <MessageBox postInfo={post} />
                <MessageBox postInfo={post} /><MessageBox postInfo={post} />
                <MessageBox postInfo={post} /><MessageBox postInfo={post} />
            </PostFeed>
            <ChatFeed>
            <ChatBox ChatName="a" Sender="JohnDoe" ChatImg="src/assets/john-doe.jpg" Message="QWERTY" ActivityDate={ Date.now()}/>
            <ChatBox ChatName="a" Sender="JohnDoe" ChatImg="src/assets/john-doe.jpg" Message="QWERTY" ActivityDate={ Date.now()}/>
            </ChatFeed>
        </StyledPage>
    )
}