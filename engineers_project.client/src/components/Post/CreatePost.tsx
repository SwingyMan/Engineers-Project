import { useState } from "react";
import styled from "styled-components";
import { usePosts } from "../../API/hooks/usePosts";

const NewPostWraper = styled.div`
  padding: 1em;
  background-color: #6085afcc;
  margin: 1em;
  border-radius: 10px;
  border: 1px solid var(--darkGrey);
  align-items: flex-start;
`;
const InputWraper = styled.div`
  display: flex;
  width: 90%;
  height: 2.4em;
  margin: 5px;
  border: 1px solid var(--light-border);
  border-radius: 10px;
  background-color: var(--whiteTransparent20);
  position: relative;
`;
const TitleInput = styled.input`
  height: 100%;
  font-size: 1.2em;
  background-color: transparent;
  border: none;
  outline: none;
  width: 100%;
  padding: 0 10px;
  color: inherit;
`;
const BodyInput = styled.input`
  height: 100%;
  font-size: 1.2em;
  background-color: transparent;
  border: none;
  outline: none;
  width: 100%;
  padding: 0 10px;
  color: inherit;
`;
export function CreatePost() {

  const [newPost, setNewPost] = useState({
    title: "",
    body: "",
    availability: 0,
  });
  const { handleAddPost } = usePosts();
  const handleInput = (e: {
    target: { name: string; value: string | 1 | 0 };
  }) => {
    const { name, value } = e.target;
    setNewPost((prev) => ({
      ...prev,
      [name]: value,
    }));
  };
  const handleSubmit = (e: { preventDefault: () => void }) => {
    e.preventDefault();
    handleAddPost.mutate({ entity: { ...newPost } });
    setNewPost({    title: "",
      body: "",
      availability: 0,})
  };
  return (
    <NewPostWraper>
      <form onSubmit={handleSubmit}>
        <InputWraper>
          <TitleInput
            type="text"
            name="title"
            placeholder="Title"
            value={newPost.title}
            onChange={handleInput}
          />
        </InputWraper>
        <InputWraper>
          <BodyInput
            type="text"
            name="body"
            value={newPost.body}
            placeholder="Body"
            onChange={handleInput}
          />
        </InputWraper>
        <input type="submit" />
      </form>
    </NewPostWraper>
  );
}
