import { IoSend } from "react-icons/io5";
import styled from "styled-components";

const CreateCommentWrapper = styled.div`
  display: flex;
  width: 100%;
  height: 2.4em;
  margin: 5px;
  border: 1px solid var(--light-border);
  border-radius: 10px;
  background-color: var(--whiteTransparent20);
`
const StyledInput = styled.input`
  height: 100%;
  font-size: 1.2em;
  background-color: transparent;
  border: none;
  outline: none;
  width: 100%;
  padding: 0 10px;
  color: inherit;
`
const SendInput = styled.input`
  background-color: var(--blue);
  border: solid 1px var(--whiteTransparent20);
  border-radius: 15px;
  height: 100%;
  width: 100%;
`
const SendWrapper = styled.div`
  position: relative;
  display: flex;
  width: 30px;
  height: 30px;
`
const StyledForm = styled.form`
  display: flex;
  align-items: center;
`
const SendIcon = styled.div`
  height: 20px;
  position:absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%,-50%);
`
export function CreateComment() {
    return (
        <StyledForm>
            <CreateCommentWrapper>
                <StyledInput type="text" placeholder="New comment"/>
            </CreateCommentWrapper>
            <SendWrapper>
                <SendIcon>
                    <IoSend size={20}/>
                </SendIcon>
                <SendInput type="send"/>
            </SendWrapper>
        </StyledForm>
    );
}