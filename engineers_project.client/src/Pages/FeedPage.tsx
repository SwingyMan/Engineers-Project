import styled from "styled-components";
import { Post } from "../components/Post/Post";
import { usePosts } from "../API/hooks/usePosts";
import { useEffect, useState } from "react";
import { useAuth } from "../Router/AuthProvider";
import { MdAdd } from "react-icons/md";
import NewPostModal from "../components/Modal/NewPostModal";
import { EditPostModal } from "../components/Modal/EditPostModal";
import { PostDTO } from "../API/DTO/PostDTO";


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
  bottom: 15px;
  right: 15px;
  border-radius: 50vh;
  border: 1px solid var(--white);
  background-color: #007aff;
  padding: 6px 18px;
  display: flex;
  align-items: center;
  cursor: pointer;
`;
const FeedContainer = styled.div`
  display: flex;
  height: 100%;
  flex: 1;
  position: relative;
`;

export function FeedPage() {
  const { data, isPending, isFetched, handleAddPost } = usePosts();

  const [openMenu, setOpenMenu] = useState<null | string>(null);
  const [isModalOpen, setOpenModal] = useState(false);
  const [postToEdit, setPostToEdit] = useState<PostDTO | null>(null);

  const handleDropdownClick = (id: string | null) => {
    setOpenMenu((prevId) => (prevId === id ? null : id));
  };
  const [isModalEditOpen, setModalEditOpen] = useState(false);

  const handleClickOutside = async (event: any) => {
    const action = event.target.getAttribute("data-action");
    const id = event.target.getAttribute("data-id");

      if (!event.target.closest(".dropdown-container")) {
      setOpenMenu(null);
      setModalEditOpen(false);
    } else {
      console.log(id, action);
      await setPostToEdit(
        data?.pages.map((page, i) => page.find((post) => post.id === id))[0]!
      );
      if (action === "edit") {
        setTimeout(() => setModalEditOpen(true));
      }
      if (action == "delete") {
      }
    }
  };

  useEffect(() => {
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);
  const { user } = useAuth();
  return (
    <FeedContainer>
      <PostFeed>
        <h3>hello, {user?.username}</h3>
        {isPending
          ? "Loading..."
          : data && (
              <>
                {data.pages.map((group, i) =>
                  group.map((post) => (
                    <Post
                      key={post.id}
                      postInfo={post}
                      isMenu={post.userId === user?.id}
                      isOpen={openMenu === post.id}
                      setIsOpen={() => handleDropdownClick(post.id)}
                    />
                  ))
                )}
              </>
            )}
      </PostFeed>
      <NewPostButton onClick={() => setOpenModal(true)}>
        <MdAdd /> Dodaj Post
      </NewPostButton>
      {/*  */}
      <NewPostModal
        isOpen={isModalOpen}
        onClose={() => setOpenModal(false)}
        initData={{ title: "", body: "", availability: 0 }}
      />
      <EditPostModal
        isOpen={isModalEditOpen}
        onClose={() => setModalEditOpen(false)}
        initData={openMenu}
      />
    </FeedContainer>
  );
}
