import styled from "styled-components";
import { Children } from "../../interface/Children";

const StyledChatFeed = styled.div`
  overflow-y: scroll;
  width: 20%;
  padding-top:10px ;
  display: flex;
  flex-direction: column;

`;
export function ChatFeed({children}:Children) {
  return (<StyledChatFeed>
    {children}
  </StyledChatFeed>);
}
