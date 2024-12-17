import styled from "styled-components";
import { getGroupImg } from "../../API/API";
import { ImageDiv } from "../Utility/ImageDiv";
import { Group } from "../../API/DTO/Group";
import { useNavigate } from "react-router";

const GroupResultHeader = styled.div`
  padding: 1em;
  background-color: #6085afcc;
  /*  */
  margin: 1em;
  border-radius: 10px;
  border: 1px solid var(--darkGrey);
  height: min-content;
  min-width: 50%;
  display: flex;
  align-items: center;
  justify-content: space-between;
`;
const GroupDescription = styled.div`
display:flex;
flex-direction: column;
`;
const HeaderInfo = styled.div`
  display: flex;
  gap: 4px;
  cursor: pointer;
`;
export function GroupCard(props: { group: Group }) {
  const navigate = useNavigate()
  return (
    <GroupResultHeader onClick={()=>navigate(`/group/${props.group.id}`)}>
      <HeaderInfo>
        <ImageDiv
          width={40}
          url={props.group.id ? getGroupImg(props.group.id) : ""}
        />
        <GroupDescription>
          <span>{props.group.name}</span>
          <span>{props.group.description}</span>
        </GroupDescription>
      </HeaderInfo>
    </GroupResultHeader>
  );
}
