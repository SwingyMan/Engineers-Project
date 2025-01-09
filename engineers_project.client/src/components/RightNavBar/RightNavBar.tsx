import styled from "styled-components";
import { ChatBox } from "./ChatBox";
import { ChatFeed } from "./ChatFeed";
import { Message } from "../../API/DTO/Message";
import { useMyChats } from "../../API/hooks/useMyChats";
import { useAuth } from "../../Router/AuthProvider";
import { getUserImg } from "../../API/API";
import { useNavigate } from "react-router";

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
  margin-bottom: 5px;
`;

export function RightNavBar() {
  const { data, isFetching ,error} = useMyChats();
  const { user } = useAuth();
  const navigate = useNavigate()
  console.log(data)
  return (
    <ChatFeed>
      <ButttonWrapper>Chats</ButttonWrapper>
      {isFetching ? (
        <>Loading</>
      ) : data && data.length === 0 ? (
        <>No Chats Yet</>
      ) : (
        data?.map((chat) => (
          <ChatBox
          key={chat.id}
          onClick={()=>navigate(`/chat/${chat.id}`)}
            ChatName={chat.name}
            Sender={chat.messages[0].user.username}
            ChatImg={getUserImg(chat.users.find((it) => it.id !== user?.id!)?.avatarFileName!)}
            Message={chat.messages[0].content}
            ActivityDate={chat.messages[0].creationDate}
            
          />
        ))
      )}

    </ChatFeed>
  );
}
