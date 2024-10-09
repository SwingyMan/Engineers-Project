import styled from "styled-components";
import { ImageDiv } from "../Utility/ImageDiv";

const StyledBox = styled.div`
    
`
interface ChatBox{
    ChatName:string,
    Sender:string,
    Message:string,
    Date:string,
    ChatImg:string,
}
export function ChatBox({ChatName,Sender,Message,Date,ChatImg}:ChatBox){
    return(
        <StyledBox>
            <ImageDiv width={40} url={ChatImg}/>
            <div>
                {ChatName}
                {Sender}:{Message} {Date}
            </div>
        </StyledBox>
    );
}