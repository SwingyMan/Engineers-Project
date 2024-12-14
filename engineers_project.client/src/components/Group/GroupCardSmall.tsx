import styled from "styled-components";
import { getGroupImg } from "../../API/API";
import { ImageDiv } from "../Utility/ImageDiv";
import { Group } from "../../API/DTO/Group";

const SmallGroupCard = styled.div.withConfig({
  shouldForwardProp: (prop) => prop !== "ishighlighted",
})<{ ishighlighted?: boolean }>`
  padding: 5px;
  background-color: ${({ ishighlighted }) =>
    ishighlighted ? "#84b3e9cc" : "#6085afcc"};
 // cursor: pointer;
  margin: 0.1em;
  border-radius: 10px;
  border: 1px solid var(--darkGrey);
  max-height: 60px;
  min-width: 50%;
  display: flex;
  align-items: center;
  justify-content: space-between;
  &:hover {
    background-color:#84b3e9cc;
  }
`;

const HeaderInfo = styled.div`
  display: flex;
  align-items: center;
  gap: 4px;
`;
export function GroupCardSmall(props: {
  handleClick: () => {};
  group: Group;
  active: boolean;
}) {
  return (
    <SmallGroupCard
      onClick={() => props.handleClick()}
      ishighlighted={props.active}
    >
      <HeaderInfo>
        <ImageDiv
          width={30}
          url={props.group.id ? getGroupImg(props.group.id) : ""}
        />

        <span>{props.group.name}</span>
      </HeaderInfo>
    </SmallGroupCard>
  );
}
