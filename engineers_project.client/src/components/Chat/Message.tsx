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

color: var(--white);
min-width: min-content;
`
const SendMessageWraper = styled.div`
   max-width: 80%;
   margin: 2px 10px;
   display: flex;
flex-direction: column;
align-items:end;
color: var(--white);
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
const MessageDate = styled.div`
    display: flex;
    align-items: center;
    gap:5px;
`
const SendMessageDate = styled.div`
        display: flex;
        flex-direction: row-reverse;
        align-items: center;
        gap:5px;
`

interface MessageInterface {
    date: number,
    message: string,

    sender: string
    //edit
}
const getFormattedDate = (date: Date): string => {
    const now = new Date();
  
    // Check if the provided date is today
    const isToday =
      date.getFullYear() === now.getFullYear() &&
      date.getMonth() === now.getMonth() &&
      date.getDate() === now.getDate();
  
    // Format hours and minutes as "HH:MM"
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
  
    if (isToday) {
      return `${hours}:${minutes}`; // Return time if the date is today
    }
  
    // Format date as "DD.MM"
    const day = date.getDate().toString().padStart(2, '0')
    ;const monthShort = date.toLocaleString([], { month: 'short' });
  
    return `${day} ${monthShort} ${hours}:${minutes}`; // Return formatted date and time
  };
export function MessageRecived({ date, message, sender }: MessageInterface) {
    return (
        <StyledMessageBox>
            <MessageWraper>
                {sender}
                <MessageDate>

                <StyledMessage>
                    {message}
                </StyledMessage>
                {getFormattedDate(new Date(date))}
                </MessageDate>
            </MessageWraper>
        </StyledMessageBox>
    );
}
export function MessageSent({ date, message, sender }: MessageInterface){
    return(
        
        <StyledSendMessageBox>
            <SendMessageWraper>
                {sender}
                <SendMessageDate>

                <StyledSendMessage>
                    {message}
                </StyledSendMessage>
                {getFormattedDate(new Date(date))}
                </SendMessageDate>
            </SendMessageWraper>
        </StyledSendMessageBox>
    )
}