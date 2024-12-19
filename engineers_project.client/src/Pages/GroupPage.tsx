import { useNavigate, useParams } from "react-router";
import { useGroupDetails } from "../API/hooks/useGroup";
import styled from "styled-components";
import { getGroupImg, getUserImg } from "../API/API";
import { ImageDiv } from "../components/Utility/ImageDiv";
import { useGroupPosts } from "../API/hooks/useGroupPosts";
import NewPostModal from "../components/Modal/NewPostModal";
import { useEffect, useState } from "react";
import { Post } from "../components/Post/Post";
import { useAuth } from "../Router/AuthProvider";
import { usePosts } from "../API/hooks/usePosts";
import { MdAdd } from "react-icons/md";
import { useMyGroups } from "../API/hooks/useMyGroups";
import { IoPencil } from "react-icons/io5";

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
`;
const GroupDescription = styled.div`
  display: flex;
  flex-direction: column;
`;
const HeaderInfo = styled.div`
  display: flex;
  gap: 4px;
  justify-content: space-between;
`;
const GroupHeaderInfo = styled.div`
  display: flex;
  gap: 4px;
`;
const Users = styled.div`
  width: 100%;
`;
const GroupFeed = styled.div`
  flex-grow: 1;
  overflow-y: scroll;
  color: var(--white);
  position: relative;
`;
const NewPostButton = styled.div`
  position: absolute;
  bottom: 10px;
  right: 10px;
  border-radius: 50vh;
  background-color: #007aff;
  padding: 4px 16px;
  display: flex;
  align-items: center;
  cursor: pointer;
`;
const UsersImgs = styled.div`
  display: flex;
  margin-left: 15px;
  & > div {
    margin-left: -15px;
  }
`;
const LeaveGroup = styled.div`
  display: flex;
  padding: 4px 8px;
  background: rgba(255, 136, 136, 0.75);
  width: fit-content;
  border-radius: 4px;
  margin: 2px;
`;
const SendRequestButton = styled.div`
  display: flex;
  padding: 4px 8px;
  width: fit-content;
  background-color: var(--blue);
  border-radius: 4px;
  cursor: pointer;
  margin: 2px;
`;
const AwaitAcceptance = styled.div`
  display: flex;
  padding: 4px 8px;
  width: fit-content;
  background: rgba(255, 255, 255, 0.6);
  color: var(--offBlack);
  border: 1px solid rgba(0, 0, 0, 0.1);
  border-radius: 4px;
  margin: 2px;
`;
const MenageGroup = styled.div`
  display: flex;
  padding: 4px 8px;
  align-items: center;
  background: gray;
  height: fit-content;
  border-radius: 4px;
  cursor: pointer;
`;
export function GroupPage() {
  const { id } = useParams();
  const [openMenu, setOpenMenu] = useState<null | string>(null);

  const handleMenuOpen = (id: string) => {
    setOpenMenu(id);
  };
  const [isModalOpen, setOpenModal] = useState(false);
  const { data: groupInfo } = useGroupDetails(id!);
  const { data: myGroups,requestToGroup } = useMyGroups();
  const [isMember, setIsMember] = useState(false);
  const [isOwner, setOwner] = useState(false);
  const [isRequestSend, setRequestSend] = useState(false);
  const { user } = useAuth();
  useEffect(() => {
    if (myGroups?.find((group) => group.id === id)) {
      setIsMember(true);
    }
    if (
      groupInfo?.groupUsers.find((gUser) => gUser.user.id === user?.id)
        ?.isOwner!
    ) {
      setOwner(true);
    }
    if (
      groupInfo?.groupUsers.find((gUser) => gUser.user.id === user?.id)
        ?.isAccepted === false
    ) {
      setRequestSend(true);
    }
  }, [myGroups, groupInfo]);

  const {
    data: groupPosts,
    isError,
    error,
    isFetched,
  } = useGroupPosts(id!, isMember);
  const navigate = useNavigate();
  return (
    <>
      <GroupFeed>
        {groupInfo && (
          <GroupCardWrapper>
            <GroupheaderWrapper>
              <HeaderInfo>
                <GroupHeaderInfo>
                  <ImageDiv
                    width={40}
                    url={groupInfo.id ? getGroupImg(groupInfo.id) : ""}
                  />
                  <GroupDescription>
                    <span>{groupInfo.name}</span>
                    <span>{groupInfo.description}</span>
                  </GroupDescription>
                </GroupHeaderInfo>
                {isOwner && (
                  <MenageGroup onClick={() => navigate(`/editGroup/${id}`)}>
                    <IoPencil size={16} />
                    Menage Group{" "}
                    {`(${
                      groupInfo.groupUsers.filter(
                        (user) => user.isAccepted === false
                      ).length
                    })`}
                  </MenageGroup>
                )}
              </HeaderInfo>
              {isMember ? (
                <LeaveGroup>Leave Group</LeaveGroup>
              ) : isRequestSend ? (
                <AwaitAcceptance>You send request</AwaitAcceptance>
              ) : (
                <SendRequestButton onClick={()=>{requestToGroup.mutate(id!)}}>Send request to join</SendRequestButton>
              )}
            </GroupheaderWrapper>
            <Users>
              users
              <UsersImgs>
                {groupInfo.groupUsers
                  .slice(0, 5)
                  .map((user) =>
                    user.isAccepted ? (
                      <ImageDiv
                        key={user.user.id}
                        width={40}
                        url={getUserImg(user.user.avatarFileName)}
                      />
                    ) : (
                      <></>
                    )
                  )}
              </UsersImgs>
              {groupInfo.groupUsers.filter((user) => user.isAccepted).length}{" "}
              Users
            </Users>
          </GroupCardWrapper>
        )}
        {isMember ? (
          <>
            {isFetched === false ? (
              <>loading</>
            ) : isError === true ? (
              <></>
            ) : groupPosts?.length !== 0 ? (
              groupPosts?.map((post) => (
                <Post
                  key={post.id}
                  postInfo={post}
                  isMenu={post.userId === user?.id}
                  isOpen={openMenu === post.id}
                  setIsOpen={() => handleMenuOpen(post.id)}
                />
              ))
            ) : (
              <>noPosts</>
            )}
            <NewPostButton onClick={() => setOpenModal(true)}>
              <MdAdd /> Add Post
            </NewPostButton>
          </>
        ) : (
          <>You need to be part of group to see Posts</>
        )}
      </GroupFeed>
      <NewPostModal
        isOpen={isModalOpen}
        onClose={() => setOpenModal(false)}
        initData={{ title: "", body: "", availability: 2, groupId: id }}
      />
    </>
  );
}
