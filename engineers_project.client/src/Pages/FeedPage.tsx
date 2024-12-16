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
  position: relative;
  & > h3 {
    margin: 20px;
  }
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
const FeedContainer = styled.div`
  display: flex;
  height: 100%;
  position: relative;
`;

export function FeedPage() {
  const { data, isPending, isFetched, handleAddPost } = usePosts();

  const [openMenu, setOpenMenu] = useState<null | string>(null);
  const [isModalOpen, setOpenModal] = useState(false);
  const handleMenuOpen = (id: string) => {
    console.log(id);
    setOpenMenu(id);
  };
  const { user } = useAuth();
  return (
    <FeedContainer>
      <PostFeed>
        <h3>hello, {user?.username}</h3>
        {isPending
          ? "Loading..."
          : data &&
            data.pages.map((group, i) =>
              group.map((post) => (
                <Post
                  key={post.id}
                  postInfo={post}
                  isMenu={post.userId === user?.id}
                  isOpen={openMenu === post.id}
                  setIsOpen={() => handleMenuOpen(post.id)}
                />
              ))
            )}
      </PostFeed>
      <NewPostButton onClick={() => setOpenModal(true)}>
        <MdAdd /> Dodaj Post
      </NewPostButton>

      <NewPostModal
        isOpen={isModalOpen}
        onClose={() => setOpenModal(false)}
        onSubmit={(data) => {
          handleAddPost.mutate(data);
        }}
        initData={{ title: "", body: "", availability: 0 }}
      />
    </FeedContainer>
  );
}
