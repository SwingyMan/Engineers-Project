import { useParams } from "react-router";
import styled from "styled-components";
import { useUsers } from "../API/hooks/useUser";
import { ImageDiv } from "../components/Utility/ImageDiv";
import { getImg } from "../API/API";
import { useUserPosts } from "../API/hooks/useUserPosts";
import { Post } from "../components/Post/Post";
import { useAuth } from "../Router/AuthProvider";
import { useState } from "react";

const ProfileHeader = styled.h1`
  color: var(--white);
`;
const ProfileCard = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
  height: fit-content;
  flex-grow:x;
  background-color: var(--whiteTransparent20);
  margin: 10px;
  box-sizing: border-box;
`;
const ProfileFeed = styled.div`
  display: flex;
  flex-direction: column;
  width: 100%;
`;
export function ProfilePage() {
  const { id } = useParams();
  const { data: userData } = useUsers(id);
  const { data: userPosts } = useUserPosts(id);
  const { user } = useAuth();

  const [openMenu, setOpenMenu] = useState<null | string>(null);
  return (
    <>
      <ProfileFeed>
        <ProfileCard>
          <ImageDiv
            width={60}
            url={userData ? getImg(userData?.avatarFileName) : ""}
            margin={"25px"}
          />

          <ProfileHeader>
            {userData ? userData.username : "Error"}
          </ProfileHeader>
        </ProfileCard>
        {userPosts ? (
          userPosts?.map((post) => (
            <Post
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
