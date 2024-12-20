import { useParams } from "react-router";
import { useChat } from "../API/hooks/useChat";
import styled from "styled-components";
import { IoSend } from "react-icons/io5";
import { MessageSent, MessageRecived } from "../components/Chat/Message";
import { useState } from "react";
import { useAuth } from "../Router/AuthProvider";
import { getUserImg } from "../API/API";
import { ImageDiv } from "../components/Utility/ImageDiv";

const StyledChatContainer = styled.div`
  display: flex;
  flex: 1;
  flex-direction: column;
  height: 100%;
`;
const CreateMessageWrapper = styled.div`
  display: flex;
  width: 100%;
  height: 2.4em;
  margin: 5px;
  border: 1px solid var(--light-border);
  border-radius: 10px;
  background-color: var(--whiteTransparent20);
`;
const StyledInput = styled.input`
  height: 100%;
  font-size: 1.2em;
  background-color: transparent;
  border: none;
  outline: none;
  width: 100%;
  padding: 0 10px;
  color: inherit;
`;
const SendInput = styled.button`
  background-color: var(--blue);
  border: solid 1px var(--whiteTransparent20);
  border-radius: 15px;
  height: 100%;
  width: 100%;
`;
const SendWrapper = styled.div`
  position: relative;
  display: flex;
  color: var(--white);
  width: 30px;
  height: 30px;
`;
const StyledForm = styled.form`
  display: flex;
  align-items: center;
`;
const SendIcon = styled.div`
  height: 20px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  pointer-events: none;
`;
const ChatSpace = styled.div`
  display: flex;
  height: 100%;
  max-height: 100%;
  flex-direction: column;
  overflow-y: scroll;
`;
const ChatHeader = styled.div`
  display: flex;
  padding: 10px;
  color: var(--white);
  gap: 5px;
`;

export function ChatPage() {
  const { id } = useParams();
  if (!id) return null;
  const handleInput = (e: { target: { name: string; value: string } }) => {
    const { name, value } = e.target;
    setMessage((prev) => ({
      ...prev,
      [name]: value,
    }));
  };
  const { user } = useAuth();
  const [message, setMessage] = useState({ chatId: id, content: "" });
  const { data, isFetching, isError, error, handleSendMessage } = useChat(id!);
  console.log(data, user?.id);
  if (isError) {
    alert(error);
  }
  const handleMessage = async (e: { preventDefault: any; }) => {
    e.preventDefault();
    setMessage((p)=>({...p,content:""}))
    handleSendMessage.mutate(message);
  };
  return (
    <StyledChatContainer>
      {data && (
        <>
          <ChatHeader>
            <ImageDiv
              width={40}
              url={getUserImg(
                data?.users.find((fuser) => fuser.id !== user?.id!)
                  ?.avatarFileName!
              )}
            />
            {data?.name}
            {/* chatname */}
            {/* options */}
          </ChatHeader>
          {/* chat */}
          <ChatSpace>
            {isFetching ? (
              <>Loading</>
            ) : data && data.messages.length === 0 ? (
              <></>
            ) : (
              data &&
              data.messages.map((message) =>
                message.userId === user?.id ? (
                  <MessageSent
                    key={message.id}
                    message={message.content}
                    date={message.creationDate}
                    sender={
                      data.users.find((fuser) => message.userId === fuser.id)
                        ?.username!
                    }
                  />
                ) : (
                  <MessageRecived
                    key={message.id}
                    message={message.content}
                    date={message.creationDate}
                    sender={
                      data.users.find((fuser) => message.userId === fuser.id)
                        ?.username!
                    }
                  />
                )
              )
            )}
          </ChatSpace>
          <div>
            <StyledForm onSubmit={handleMessage}>
              <CreateMessageWrapper>
                <StyledInput
                  type="text"
                  placeholder="New message"
                  name="content"
                  value={message.content}
                  onChange={handleInput}
                />
              </CreateMessageWrapper>
              <SendWrapper>
                <SendIcon>
                  <IoSend size={20} />
                </SendIcon>
                <SendInput />
              </SendWrapper>
            </StyledForm>
          </div>
        </>
      )}
    </StyledChatContainer>
  );
}
