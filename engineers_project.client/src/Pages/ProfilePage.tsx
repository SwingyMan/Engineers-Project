import { useNavigate, useParams } from "react-router";
import styled from "styled-components";
import { useUsers } from "../API/hooks/useUser";
import { ImageDiv } from "../components/Utility/ImageDiv";
import { getUserImg } from "../API/API";
import { useUserPosts } from "../API/hooks/useUserPosts";
import { Post } from "../components/Post/Post";
import { useAuth } from "../Router/AuthProvider";
import { useState } from "react";
import { IoPencil } from "react-icons/io5";
import { useFriends } from "../API/hooks/useFriends";
import { getOrCreateChat } from "../API/services/chat.service";
import { FriendList } from "../API/services/friends.service";

const ProfileHeader = styled.h1`
  color: var(--white);
`;
const ProfileCard = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
  height: fit-content;
  justify-content: space-between;
  background-color: var(--whiteTransparent20);
  margin: 10px;
  box-sizing: border-box;
  padding-right: 30px;
`;
const ProfileFeed = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
`;
const MenageUser = styled.div`
  display: flex;
  padding: 4px 8px;
  align-items: center;
  background: gray;
  height: fit-content;
  border-radius: 4px;
  cursor: pointer;
`;
const UserInfo = styled.div`
  display: flex;
  align-items: center;
`;
const Controls = styled.div<{ color: string }>`
  display: flex ;
  background-color: ${(p) => p.color};
  padding: 4px 8px;
  align-items:center;
  cursor: pointer;
`

export function ProfilePage() {

  const navigate = useNavigate();
  const { id } = useParams();
  const { data: userData } = useUsers(id!);
  const { data: userPosts } = useUserPosts(id!);
  const { user } = useAuth();
  const [openMenu, setOpenMenu] = useState<null | string>(null);
  const { data: friendsData, handlAcceptFriend, handleRequestFriend } = useFriends();
  const handleOpenChat = async () => {
    getOrCreateChat(id!).then(
      () => navigate("/chat")
    )
  }
  const findUser = (data: FriendList, id: string): string => {
    for (let state in data) {
      const user = data[state as keyof FriendList].find((user) => user.id === id)
      if (user) {
        return state
      }

    }
    return "none"

  }
  return (
    <>
      <ProfileFeed>
        <ProfileCard>
          <UserInfo>
            <ImageDiv
              width={60}
              url={userData ? getUserImg(userData?.avatarFileName) : ""}
              margin={"25px"}
            />

            <ProfileHeader>{userData ? userData.username : ""}</ProfileHeader>
          </UserInfo>
          {id === user?.id ? (
            <MenageUser onClick={() => navigate("/editProfile")}>
              <IoPencil size={16} /> Edit Profile
            </MenageUser>
          ) : (
            friendsData &&
            (findUser(friendsData, user?.id!) == "friends" ? (
              <Controls color="var(--blue)" onClick={() => { handleOpenChat() }}>
                Send message
              </Controls >
            ) : findUser(friendsData, user?.id!) === "send" ? (
              <Controls color="rgba(255, 255, 255, 0.6)" >Request send</Controls>
            ) : findUser(friendsData, user?.id!) === "recived" ? (
              <Controls color="var(--blue)" onClick={() => { handlAcceptFriend.mutate(id!) }}>Accept friend request</Controls>
            ) : (
              <Controls color="var(--blue)" onClick={() => { handleRequestFriend.mutate(id!) }}>Send friend request</Controls>
            ))
          )}
        </ProfileCard>
        {userPosts ? (
          userPosts?.map((post) => (
            <Post
              key={post.id}
              postInfo={post}
              isMenu={post.userId === user?.id}
              isOpen={openMenu === post.id}
              setIsOpen={setOpenMenu}
            />
          ))
        ) : (
          <div>no posts yet</div>
        )}
      </ProfileFeed>
    </>
  );
}
