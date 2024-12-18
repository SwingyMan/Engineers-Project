import { FaHome } from "react-icons/fa";
import { useNavigate, useParams } from "react-router";
import styled from "styled-components";
import { useMyGroups } from "../../API/hooks/useMyGroups";
import { GroupCardSmall } from "../Group/GroupCardSmall";

const NavBarWrapper = styled.div`
  display: flex;
  width: 15%;
  height: 100%;
  color: var(--white);
  flex-direction: column;
  padding: 10px;
  gap: 15px;
`;
const ButttonWrapper = styled.div<{ avtive?: boolean }>`
  display: flex;
  padding: 5px 10px;
  height: fit-content;
  width: -webkit-fill-available;
  align-content: center;
  place-items: center;
  background-color: gray;
  border-radius: 4px;
  min-width: 0px;
`;
const MainNavMenu = styled.div`
  display: flex;
`;
const GroupWrapper = styled.div`
  display: flex;
  flex-direction: column;
  text-align: center;
`;
const Button = styled.div`
  background-color: gray;
  height: fit-content;
  display: flex;
  padding: 5px 10px;
  width: 100%;
  justify-content: center;
  border-radius: 4px;
  cursor: pointer;
`;
const ButtonWrapper = styled.div`
  display: flex;
  align-items: center;
  flex-direction: column;
`;
export function LeftNavBar() {
  const navigate = useNavigate();
  const { id } = useParams();
  const { data, isFetching } = useMyGroups();

  return (
    <NavBarWrapper>
      <MainNavMenu>
        <ButttonWrapper onClick={() => navigate("/")}>
          <FaHome /> Home
        </ButttonWrapper>
      </MainNavMenu>
      <div>
        <ButttonWrapper>Your Groups</ButttonWrapper>
        <GroupWrapper>
          {isFetching && <>loading</>}
          {data !== null &&
            (data?.length !== 0 ? (
              data?.map((group) => (
                <GroupCardSmall
                  key={group.id}
                  handleClick={async () => navigate(`/group/${group.id}`)}
                  group={group}
                  active={group.id === id}
                />
              ))
            ) : (
              <>You are not a part of any group</>
            ))}
          <ButtonWrapper>
            <Button>Join Group</Button>
            or
            <Button onClick={()=>navigate('/newGroup')}>Create Group</Button>
          </ButtonWrapper>
        </GroupWrapper>
      </div>
    </NavBarWrapper>
  );
}
