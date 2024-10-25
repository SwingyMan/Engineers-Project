import styled from "styled-components";

const StyledMessageBox = styled.div<{ send: number }>`
   // background-color: ${(props) => props.send == 1 ? "red" : "blue"};
 display: flex;
 flex-direction: ${(props) => props.send == 1 ? "row-reverse" : "row "};

`
const MessageWraper = styled.div<{ send: number }>`
   max-width: 80%;
   margin: 2px 10px;
   display: flex;
flex-direction: column;
align-items:${(props) => props.send == 1 ? "end" : "start"} ;
min-width: min-content;


`
const StyledMessage = styled.div<{ send: number }>`
background-color: ${(props) => props.send == 1 ? "black" : "white"};
color: ${(props) => props.send == 1 ? "white" : "black"};
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
            <MessageWraper send={send}>
            {sender}
            <StyledMessage send={send}>
                {message}
            </StyledMessage>
            {date}
            </MessageWraper>
        </StyledMessageBox>
    );
}