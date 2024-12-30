import { useState } from "react";
import { IoSend } from "react-icons/io5";
import styled from "styled-components";
import { useComments } from "../../API/hooks/useComments";

const CreateCommentWrapper = styled.div`
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
  cursor: pointer;
  background-color: var(--blue);
  border: solid 1px var(--whiteTransparent20);
  border-radius: 15px;
  height: 100%;
  width: 100%;
`;
const SendWrapper = styled.div`
  position: relative;
  display: flex;
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
export function CreateComment({ id }: { id: string }) {
  const [newComment, setNewComment] = useState({
    postId: id,
    content: "",
  });
  const handleInput = (e: { target: { name: string; value: string } }) => {
    const { name, value } = e.target;
    setNewComment((prev) => ({
      ...prev,
      [name]: value,
    }));
  };
  const { handleAddComment } = useComments(id);
  const SubmitComment = (e: { preventDefault: () => void }) => {
    e.preventDefault();
    handleAddComment.mutate(newComment);
    setNewComment({ ...newComment, content: "" });
  };
  return (
    <StyledForm onSubmit={SubmitComment}>
      <CreateCommentWrapper>
        <StyledInput
          type="text"
          placeholder="New comment"
          name="content"
          value={newComment.content}
          onChange={handleInput}
        />
      </CreateCommentWrapper>
      <SendWrapper>
        <SendIcon>
          <IoSend size={20} />
        </SendIcon>
        <SendInput onClick={() => {}} />
      </SendWrapper>
    </StyledForm>
  );
}
