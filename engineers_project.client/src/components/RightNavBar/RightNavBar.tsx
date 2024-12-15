import styled from "styled-components";
import { ChatBox } from "./ChatBox";
import { ChatFeed } from "./ChatFeed";

const ButttonWrapper = styled.div<{ avtive?: boolean }>`
  display: flex;
  padding: 5px 10px;
  height: fit-content;
  width: -webkit-fill-available;
  align-content: center;
  place-items: center;
  color: var(--white);
  background-color: gray;
  border-radius: 4px 4px 0px 0px;
  min-width: 0px;
  margin-bottom:5px;
`;
export function RightNavBar(){

    return(
        <ChatFeed>
        <ButttonWrapper>
          Chats
        </ButttonWrapper>
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
    )
}