import styled from "styled-components";
import { ImageDiv } from "../Utility/ImageDiv";

const StyledBox = styled.div`
  background-color: rgba(255, 255, 255, 0.2);
  border-bottom: 1px solid rgba(255, 255, 255, 0.6);
  display: flex;
  padding: 4px;
  color : white;
`;
const Content = styled.div`
  padding-left: 2px;
  & > b {
    display: block;
  }
`;
interface ChatBox {
  ChatName: string;
  Sender: string;
  Message: string;
  ActivityDate: number;
  ChatImg: string;
}
function formatDate(inputDate: Date) {

  const now = new Date();
  // Check if input date is today
  const isToday =
    inputDate.getDate() === now.getDate() &&
    inputDate.getMonth() === now.getMonth() &&
    inputDate.getFullYear() === now.getFullYear();

  if (isToday) {
    // Return hours and minutes
    return inputDate.toLocaleTimeString([], {
      hour: "2-digit",
      minute: "2-digit",
    });
  }

  const oneYearAgo = new Date();
  oneYearAgo.setFullYear(now.getFullYear() - 1);

  // Check if input date is within the last year
  if (inputDate >= oneYearAgo) {
    // Return day and month
    return inputDate.toLocaleDateString([], { day: "2-digit", month: "short" });
  }

  // If older than a year, return month and year
  return inputDate.toLocaleDateString([], { month: "short", year: "numeric" });
}
export function ChatBox({
  ChatName,
  Sender,
  Message,
  ActivityDate,
  ChatImg,
}: ChatBox) {
  return (
    <StyledBox>
      <ImageDiv width={40} url={ChatImg} />
      <Content>
        <b>{ChatName}</b>
        {Sender}: {Message} {formatDate(new Date(ActivityDate))}
      </Content>
    </StyledBox>
  );
}
