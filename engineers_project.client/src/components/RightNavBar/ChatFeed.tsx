import styled from "styled-components";
import { Children } from "../../interface/Children";

const StyledChatFeed = styled.div`
  overflow-y: scroll;
  width: 20%;
  padding-top:10px ;
  padding-right: 5px;
  display: flex;
  flex-direction: column;
  gap: 2px;
`;
export function ChatFeed({children}:Children) {
  return (<StyledChatFeed>
    {children}
  </StyledChatFeed>);
}
