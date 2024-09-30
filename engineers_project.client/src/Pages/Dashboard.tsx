import { Outlet } from "react-router";
import { MessageBox } from "../components/MessageBox/MessageBox";
import { Post } from "../components/MessageBox/PostClass";
import { TopNavBar } from "../components/TopNavBar/TopNavBar";
import styled from "styled-components";

let post = new Post(
  "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Consectetur voluptate nostrum dolores quidem distinctio placeat, laboriosam, sed fugit eum expedita, sapiente repellendus enim. Maxime iure possimus repellendus tempora eum recusandae!",
  "John Doe",
  "https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Classes",
  Date.now(),
  "src/assets/john-doe.jpg",
  2
);
const MessageFeed = styled.div`
overflow-y: scroll;
/* hide elements */
/* visibility: hidden; */
`

export default function Dashboard() {
  return (
    <>
      <>
        <TopNavBar />
        <MessageFeed>
          <MessageBox
            postInfo={new Post("a", "a", "a", 1, "src/assets/john-doe.jpg", 1)}
          />
          <MessageBox postInfo={post} />
          <MessageBox postInfo={post} /><MessageBox postInfo={post} />
        </MessageFeed>
      </>
      <div style={{ display: "flex" }}>
        <Outlet />
      </div>
    </>
  );
}
