import styled from "styled-components";

const StyledMessageBox = styled.div<{ send: number }>`
   // background-color: ${(props) => props.send == 1 ? "red" : "blue"};
 display: flex;
 flex-direction: ${(props) => props.send == 1 ? "row-reverse" : "row "};

`
const MessageWraper = styled.div`
   max-width: 80%;
`
const StyledMessage = styled.div<{ send: number }>`
    background-color: ${(props) => props.send == 1 ? "black" : "white"};
    color: ${(props) => props.send == 1 ? "white" : "black"};
width: min-content;

margin:2px;
padding: 5px 10px;
border-radius: 12px;
`
interface MessageInterface {
    date: number,
    message: string,
    send: number
    sender: string
    //edit
}
export function Message({ date, message, send, sender }: MessageInterface) {
    return (
        <StyledMessageBox send={send}>
            <MessageWraper>
            {sender}
            <StyledMessage send={send}>
                {message}
            </StyledMessage>
            {date}
            </MessageWraper>
        </StyledMessageBox>
    );
}