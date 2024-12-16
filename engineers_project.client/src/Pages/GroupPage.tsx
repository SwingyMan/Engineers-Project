import { useParams } from "react-router";
import { useGroupDetails } from "../API/hooks/useGroup";
import styled from "styled-components";
import { getGroupImg, getUserImg } from "../API/API";
import { ImageDiv } from "../components/Utility/ImageDiv";
import { useGroupPosts } from "../API/hooks/useGroupPosts";
import NewPostModal from "../components/Modal/Modal";
import { useState } from "react";
import { Post } from "../components/Post/Post";
import { useAuth } from "../Router/AuthProvider";
import { usePosts } from "../API/hooks/usePosts";
import { MdAdd } from "react-icons/md";

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
  position: relative;
`;
const NewPostButton = styled.div`
position: absolute;
bottom:10px;
right: 10px;
border-radius: 50vh;
background-color:#007aff;
padding: 4px 16px;
display: flex;
align-items: center;
cursor: pointer;
`
export function GroupPage() {
  const { id } = useParams();
  const [openMenu,setOpenMenu] = useState<null|string>(null)
  const handleMenuOpen=(id:string)=>{
    console.log(id)
    setOpenMenu(id)
  }
  const { handleAddPost } = usePosts();
  const [isModalOpen,setOpenModal]=useState(false)
  const { data: groupInfo } = useGroupDetails(id!);
  const {data:groupPosts ,isError,error, isFetched} = useGroupPosts(id!);

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
                <NewPostButton onClick={()=>setOpenModal(true)}>
                  <MdAdd /> Add Post
                </NewPostButton>
      </GroupFeed>
      <NewPostModal isOpen={isModalOpen} onClose={()=>setOpenModal(false)} onSubmit={(data: {})=>{handleAddPost.mutate(data)}}initData={{title:"",body:"",availability:2, groupId:id}}/>

    </>
  );
}
