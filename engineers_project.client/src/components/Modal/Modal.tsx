import React, { useState } from "react";
import styled from "styled-components";
import { NewPost } from "../../API/DTO/NewPost";

// Styled components
const Overlay = styled.div`
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1;
`;

const ModalContainer = styled.div`
  background-color: rgb(96, 133, 175);
  padding: 20px;
  border-radius: 8px;
  width: 400px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  color: var(--white);
`;

const Header = styled.h2`
  margin: 0 0 20px;
  font-size: 1.5rem;
`;

const Form = styled.form`
  display: flex;
  flex-direction: column;
  gap: 15px;
`;

const Input = styled.input`
  padding: 10px;
  background-color: var(--whiteTransparent20);
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  color: var(--white);
`;

const TextArea = styled.textarea`
  padding: 10px;
  font-family: inherit;
  font-size: inherit;
  background-color: var(--whiteTransparent20);
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  resize: none;
  height: 100px;
  color: var(--white);
`;

const Select = styled.select`
  padding: 10px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  color: var(--white);
  background-color: var(--whiteTransparent20);
  option {
    color: black;
  }
`;

const ButtonGroup = styled.div`
  display: flex;
  justify-content: flex-end;
  gap: 10px;
`;

const Button = styled.button`
  padding: 10px 15px;
  font-size: 1rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  background-color: ${(props) =>
    props.color === "primary" ? "#007BFF" : "#6c757d"};
  color: var(--white);
  &:hover {
    background-color: ${(props) =>
      props.color === "primary" ? "#0056b3" : "#5a6268"};
  }
`;

// Modal Component
interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSubmit: Function;
  initData: NewPost;
}
interface FileDetails {
  file: File;
  preview: string;
  error: string | null;
}

const NewPostModal: React.FC<ModalProps> = ({
  isOpen,
  onClose,
  onSubmit,
  initData,
}) => {
  const [files, setFiles] = useState<FileDetails[]>([]);
  const MAX_FILE_SIZE = 2 * 1024 * 1024; // 2MB

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFiles = event.target.files;
    if (!selectedFiles) return;

    const updatedFiles: FileDetails[] = Array.from(selectedFiles).map(
      (file) => {
        const isFileTooLarge = file.size > MAX_FILE_SIZE;
        return {
          file,
          preview: !isFileTooLarge ? URL.createObjectURL(file) : "",
          error: isFileTooLarge
            ? `File "${file.name}" exceeds the 2MB size limit.`
            : null,
        };
      }
    );

    setFiles((prevFiles) => [...prevFiles, ...updatedFiles]);
  };

  const handleRemoveFile = (index: number) => {
    setFiles((prevFiles) => {
      const updatedFiles = [...prevFiles];
      updatedFiles.splice(index, 1);
      return updatedFiles;
    });
  };
  const initPost: NewPost = { ...initData };
  console.log(initPost);
  const [newPost, setNewPost] = useState(initPost);
  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const NewPost = {
      entity: newPost!,
    };
    onSubmit(NewPost);
    onClose();
  };
  const handleChange = (
    e: React.ChangeEvent<
      HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement
    >
  ) => {
    const { name, value } = e.target;

    setNewPost({
      ...newPost,
      [name]: name === "availability" ? Number(value) : value,
    });
  };

  if (!isOpen) return null;

  return (
    <Overlay
      onClick={() => {
        onClose(), setNewPost(initPost);
      }}
    >
      <ModalContainer
        onClick={(e) => {
          e.stopPropagation();
        }}
      >
        <Header>Create Post</Header>
        <Form onSubmit={handleSubmit}>
          <Input
            type="text"
            placeholder="Title"
            name="title"
            value={newPost.title}
            onChange={handleChange}
            required
          />
          <TextArea
            placeholder="Content"
            value={newPost.body}
            name="body"
            onChange={handleChange}
            required
          />
          {initData.availability === 2 ? (
            <></>
          ) : (
            <Select
              value={newPost.availability}
              onChange={handleChange}
              name="availability"
            >
              <option value={0}>Public</option>
              <option value={1}>Private</option>
            </Select>
          )}
          <div>
            <form>
              <label htmlFor="fileInput">
                Upload files (Max size: 50MB each):
              </label>
              <input
                type="file"
                id="fileInput"
                multiple
                accept=".png,.jpg,.jpeg" // Restrict accepted file types
                onChange={handleFileChange}
              />
            </form>

            <div>
              <h3>Uploaded Files:</h3>
              <div style={{ display: "flex", flexWrap: "wrap", gap: "1rem" }}>
                {files.map((fileDetail, index) => (
                  <div
                    key={index}
                    style={{
                      border: "1px solid #ccc",
                      padding: "10px",
                      textAlign: "center",
                    }}
                  >
                    {fileDetail.preview ? (
                      <img
                        src={fileDetail.preview}
                        alt={fileDetail.file.name}
                        style={{ maxWidth: "100px", maxHeight: "100px" }}
                      />
                    ) : (
                      <p>No preview available</p>
                    )}
                    <p>{fileDetail.file.name}</p>
                    {fileDetail.error && (
                      <p style={{ color: "red" }}>{fileDetail.error}</p>
                    )}
                    <button
                      type="button"
                      onClick={() => handleRemoveFile(index)}
                    >
                      Remove
                    </button>
                  </div>
                ))}
              </div>
            </div>
          </div>
          <ButtonGroup>
            <Button type="button" onClick={onClose} color="secondary">
              Cancel
            </Button>
            <Button type="submit" color="primary">
              Submit
            </Button>
          </ButtonGroup>
        </Form>
      </ModalContainer>
    </Overlay>
  );
};

export default NewPostModal;
