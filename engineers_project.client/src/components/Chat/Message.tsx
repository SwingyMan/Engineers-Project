import styled from "styled-components";
const StyledMessageBox = styled.div`
 display: flex;
 flex-direction: row ;

`
const StyledSendMessageBox = styled.div`
 display: flex;
 flex-direction:row-reverse;

`
const MessageWraper = styled.div`
   max-width: 80%;
   margin: 2px 10px;
   display: flex;
flex-direction: column;
align-items:start;
min-width: min-content;
`
const SendMessageWraper = styled.div`
   max-width: 80%;
   margin: 2px 10px;
   display: flex;
flex-direction: column;
align-items:end;
min-width: min-content;
`
const StyledMessage = styled.div`
background-color:white;
color: black;
margin:2px;
padding: 5px 10px;
border-radius: 12px;
`
const StyledSendMessage = styled.div`
background-color: black;
color: white;
margin:2px;
padding: 5px 10px;
border-radius: 12px;
`

interface MessageInterface {
    date: number,
    message: string,

    sender: string
    //edit
}
export function MessageRecived({ date, message, sender }: MessageInterface) {
    return (
        <StyledMessageBox>
            <MessageWraper>
                {sender}
                <StyledMessage>
                    {message}
                </StyledMessage>
                {date}
            </MessageWraper>
        </StyledMessageBox>
    );
}
export function MessageSent({ date, message, sender }: MessageInterface){
    return(
        
        <StyledSendMessageBox>
            <SendMessageWraper>
                {sender}
                <StyledMessage>
                    {message}
                </StyledMessage>
                {date}
            </SendMessageWraper>
        </StyledSendMessageBox>
    )
}