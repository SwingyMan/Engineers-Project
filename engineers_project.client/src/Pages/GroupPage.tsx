import { useParams } from "react-router"
import { useGroupDetails } from "../API/hooks/useGroup"
import styled from "styled-components";
import { getGroupImg, getUserImg } from "../API/API";
import { ImageDiv } from "../components/Utility/ImageDiv";

const GroupCardWrapper = styled.div`
  padding: 1em;
  background-color: #6085afcc;
  /*  */
  margin: 1em;
  border-radius: 10px;
  border: 1px solid var(--darkGrey);
  height: min-content;
  min-width: 50%;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
`;
const GroupheaderWrapper = styled.div`
    width: 100%;
`
const GroupDescription = styled.div`
display:flex;
flex-direction: column;
`;
const HeaderInfo = styled.div`
  display: flex;
  gap: 4px;
  cursor: pointer;
`;
const Users = styled.div`
    width: 100%;
`
export function GroupPage(){
    const {id } = useParams()
    const {data} = useGroupDetails(id!)
 console.log(data?.groupUsers)
    return(<>
    {data&&    <GroupCardWrapper>
        <GroupheaderWrapper>

          <HeaderInfo>
            <ImageDiv
              width={40}
              url={data.id ? getGroupImg(data.id) : ""}
              />
            <GroupDescription>
              <span>{data.name}</span>
              <span>{data.description}</span>
            </GroupDescription>
          </HeaderInfo>
              </GroupheaderWrapper>
<Users>users
    {data.groupUsers.map((user)=><ImageDiv key={user.user.id} width={30} url={getUserImg(user.user.avatarFileName) }/>)}
</Users>
        </GroupCardWrapper>}</>)
}