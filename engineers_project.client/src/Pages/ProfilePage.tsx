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
  padding-right:30px;
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
  align-items:center;
`
export function ProfilePage() {
  const navigate = useNavigate();
  const { id } = useParams();
  const { data: userData } = useUsers(id!);
  const { data: userPosts } = useUserPosts(id!);
  const { user } = useAuth();
  const [openMenu, setOpenMenu] = useState<null | string>(null);

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
          ):<div>
            {/* addfriend */}
            {/* waiting */}
            {/* friends */}
            </div>}
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
