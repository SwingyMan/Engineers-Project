import styled from "styled-components";
import { Post } from "../components/Post/Post";
import { ChatBox } from "../components/RightNavBar/ChatBox";
import { ChatFeed } from "../components/RightNavBar/ChatFeed";
import { usePosts } from "../API/hooks/usePosts";
import { useState } from "react";
import { useAuth } from "../Router/AuthProvider";
import { MdAdd } from "react-icons/md";
import NewPostModal from "../components/Modal/Modal";

const PostFeed = styled.div`
  flex: 1;
  overflow-y: scroll;
  color: var(--white);
  position:relative;
  &>h3{
    margin:20px
  }
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
const ButttonWrapper = styled.div<{ avtive?: boolean }>`
  display: flex;
  padding: 5px 10px;
  height: fit-content;
  width: -webkit-fill-available;
  align-content: center;
  place-items: center;
  color: var(--white);
  background-color: gray;
  border-radius: 4px 4px 0px 0px;
  min-width: 0px;
  margin-bottom:5px;
`;

export function FeedPage() {
  const { data, isPending, isFetched , handleAddPost } = usePosts();

  const [openMenu, setOpenMenu] = useState<null | string>(null)
  const [isModalOpen,setOpenModal]=useState(false)
  const handleMenuOpen = (id: string) => {
    console.log(id)
    setOpenMenu(id)
  }
  const { user } = useAuth()
  return (
    <>
      <PostFeed>
        <h3>hello, {user?.username}</h3>
        {isPending ? "Loading..." : (data &&
          data.pages.map((group, i) =>
            group.map((post) => <Post key={post.id} postInfo={post} isMenu={post.userId === user?.id} isOpen={openMenu === post.id} setIsOpen={() => handleMenuOpen(post.id)} />)
          ))}
        <NewPostButton onClick={()=>setOpenModal(true)}>
          <MdAdd /> Dodaj Post
        </NewPostButton>
      </PostFeed>

      <NewPostModal isOpen={isModalOpen} onClose={()=>setOpenModal(false)} onSubmit={(data)=>{handleAddPost.mutate(data)}}initData={{title:"",body:"",availability:0}}/>

    </>
  );
}
