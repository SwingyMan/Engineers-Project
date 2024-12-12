import styled from "styled-components";
import { getUserImg } from "../../API/API";
import { ImageDiv } from "../Utility/ImageDiv";
import { User } from "../../API/DTO/User";

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
const HeaderInfo = styled.div`
    display: flex;
    gap:4px;
    cursor: pointer;
`
export function UserCard(props:{user:User}){
    return(
        <GroupResultHeader>
            <HeaderInfo>
            <ImageDiv width={40} url={props.user.avatarFileName ? getUserImg(props.user.avatarFileName) : ""} />
            {props.user.username}
            </HeaderInfo>
        </GroupResultHeader>
    )
}