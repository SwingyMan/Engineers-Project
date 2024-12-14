import { useParams } from "react-router";
import { useGroupDetails } from "../API/hooks/useGroup";
import styled from "styled-components";
import { getGroupImg, getUserImg } from "../API/API";
import { ImageDiv } from "../components/Utility/ImageDiv";
import { useGroupPosts } from "../API/hooks/useGroupPosts";
import Modal from "../components/Modal/Modal";
import { useState } from "react";
import { Post } from "../components/Post/Post";
import { useAuth } from "../Router/AuthProvider";

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
  cursor: pointer;
`;
const Users = styled.div`
  width: 100%;
`;
const GroupFeed = styled.div`
  flex-grow: 1;
  overflow-y: scroll;
  color: var(--white);
`;
export function GroupPage() {
  const { id } = useParams();
  const [openMenu,setOpenMenu] = useState<null|string>(null)
  const handleMenuOpen=(id:string)=>{
    console.log(id)
    setOpenMenu(id)
  }
  const { data: groupInfo } = useGroupDetails(id!);
  const {data:groupPosts ,isError,error, isFetched} = useGroupPosts(id!);
  const [isModalOpen,setModalOpen]=useState(true)
  console.log(error)
    const {user} = useAuth()
  return (
    <>
      <GroupFeed>
        {groupInfo && (
          <GroupCardWrapper>
            <GroupheaderWrapper>
              <HeaderInfo>
                <ImageDiv
                  width={40}
                  url={groupInfo.id ? getGroupImg(groupInfo.id) : ""}
                />
                <GroupDescription>
                  <span>{groupInfo.name}</span>
                  <span>{groupInfo.description}</span>
                </GroupDescription>
              </HeaderInfo>
            </GroupheaderWrapper>
            <Users>
              users
              {groupInfo.groupUsers.slice(0,5).map((user) => (
                <ImageDiv
                  key={user.user.id}
                  width={30}
                  url={getUserImg(user.user.avatarFileName)}
                />
              ))}
              {groupInfo.groupUsers.length} Users
            </Users>
          </GroupCardWrapper>
        )}
        { isFetched===false? <>loading</>:(isError===true?<></>:groupPosts?.length!==0?groupPosts?.map((post)=><Post key={post.id} postInfo={post}  isMenu={post.userId===user?.id} isOpen={openMenu===post.id} setIsOpen={()=>handleMenuOpen(post.id)}/>):<>noPosts</>)}
      </GroupFeed>
      {/* <Modal isOpen={isModalOpen} onClose={()=>setModalOpen(false)}/> */}
    </>
  );
}
