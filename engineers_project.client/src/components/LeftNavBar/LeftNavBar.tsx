import { FaHome } from "react-icons/fa";
import { useNavigate, useParams } from "react-router";
import styled from "styled-components";
import { useMyGroups } from "../../API/hooks/useMyGroups";
import { GroupCardSmall } from "../Group/GroupCardSmall";
import { useFriends } from "../../API/hooks/useFriends";
import { useState } from "react";
import { UserCard } from "../User/UserCard";

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
  background-color: #808080;
  height: fit-content;
  display: flex;
  padding: 5px 10px;
  width: 100%;
  justify-content: center;
  border-radius: 4px;
  cursor: pointer;
`;
const ControlWrapper = styled.div`
  display: flex;
`;
const FriendsWrapper = styled.div`
  &>div{
    margin:2px;
  }
`
const Control = styled.div<{ active: boolean }>`
  display: flex;
  width: 100%;
  padding: 4px;
  background-color: ${(p) => (p.active === true ? "#929292" : "gray")};
  border-radius: 4px 4px 0px 0px;
`;
const ButtonWrapper = styled.div`
  display: flex;
  align-items: center;
  flex-direction: column;
`;
export function LeftNavBar() {
  const navigate = useNavigate();
  const { id } = useParams();
  const { data: GroupData, isFetching } = useMyGroups();
  const { data: FriendsData, isFetching: fechingFriends } = useFriends();
  const [friends, setFriends] = useState(true);
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
          {GroupData !== null &&
            (GroupData?.length !== 0 ? (
              GroupData?.map((group) => (
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
            <Button onClick={() => navigate("/groups")}>Join Group</Button>
            or
            <Button onClick={() => navigate("/newGroup")}>Create Group</Button>
          </ButtonWrapper>
        </GroupWrapper>
      </div>
      <div>
        <GroupWrapper>
          {fechingFriends && <>loading</>}
          <ControlWrapper>
            <Control active={friends} onClick={() => setFriends(true)}>
              Friends
            </Control>
            <Control active={!friends} onClick={() => setFriends(false)}>
              Requests
            </Control>
          </ControlWrapper>
          <FriendsWrapper>
            {FriendsData !== null &&
              (FriendsData?.friends.length !== 0 && friends ? (
                FriendsData?.friends.map((friend) => (
                  <UserCard key={friend.id} user={friend} />
                ))
              ) : FriendsData?.received.length !== 0 && friends === false ? (
                FriendsData?.received.map((friend) => (
                  <UserCard key={friend.id} user={friend} />
                ))
              ) : (
                <>You have no friends yet</>
              ))}
          </FriendsWrapper>
        </GroupWrapper>
      </div>
    </NavBarWrapper>
  );
}
