import React, { useState } from "react";
import styled from "styled-components";
import { NewPost } from "../../API/DTO/NewPost";
import { createPost } from "../../API/services/posts.service";
import { postAttachment } from "../../API/API";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { usePosts } from "../../API/hooks/usePosts";

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
const DeleteButton = styled.button`
  background-color: #dd3d3d;
  padding: 4px;
  border-color: var(--whiteTransparent20);
  border-radius: 4px;
`;
const FileWrapper = styled.div`
  display: flex;
  flex-wrap: nowrap;
  gap: 1rem;
  overflow-x: scroll;
  &::-webkit-scrollbar {
    height: 8px;
  }

  &::-webkit-scrollbar-thumb {
    background: #ccc;
    border-radius: 4px;
  }

  &::-webkit-scrollbar-track {
    background: #f0f0f0;
  }
`;
const FilePreview = styled.div`
  border: 1px solid #ccc;
  padding: 10px;
  display: flex;
  flex-direction: column;
  word-break: break-all;
  gap: 5px;
  max-width: 150px;
  text-overflow: clip;
  text-align: center;
  align-items: center;
`;

// Modal Component
interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  initData: NewPost;
}
interface FileDetails {
  file: File;
  preview: string;
  error: string | null;
}

const useUploadAttachment = () => {
  return useMutation({mutationFn:(formData: FormData) =>
    postAttachment("Attachment/Post", formData)}
  );
};
const NewPostModal: React.FC<ModalProps> = ({ isOpen, onClose, initData }) => {
  const [files, setFiles] = useState<FileDetails[]>([]);
  const MAX_FILE_SIZE = 4 * 1024 * 1024; // 4MB
  const uploadAttachmentMutation = useUploadAttachment();

  const queryClient = useQueryClient();
  const { handleAddPost } = usePosts();
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
            ? `File "${file.name}" exceeds the 4MB size limit.`
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
  const [newPost, setNewPost] = useState(initPost);
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const NewPost = {
      entity: newPost!,
    };
    handleAddPost.mutate(NewPost, {
      onSuccess: async (res) => {
        const newPostId =res.id;

        // Prepare and send the file upload requests
        const uploadPromises = files
          .filter((file) => !file.error) // Only upload files without errors
          .map((fileDetail) => {
            const formData = new FormData();
            formData.append("AttachmentDto.file", fileDetail.file);
            formData.append("AttachmentDto.PostID", newPostId);

            return uploadAttachmentMutation.mutateAsync(formData);
          });

        try {
          // Wait for all file uploads to complete
          await Promise.all(uploadPromises);
          // Invalidate the query to refetch the updated posts
          queryClient.invalidateQueries({ queryKey: ["Posts"] });
          onClose();
        } catch (uploadError) {
          console.error("File upload failed:", uploadError);
        }
      },
      onError: (error) => {
        console.error("Post creation failed:", error);
      },
    });
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
        onClose();
        setNewPost(initPost);
        setFiles([]);
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
            <label htmlFor="fileInput">
              Upload files (Max size: 4MB each):
            </label>
            <input
              type="file"
              id="fileInput"
              multiple
              onChange={handleFileChange}
            />

            <div>
              <h3>Uploaded Files:</h3>
              <FileWrapper>
                {files.map((fileDetail, index) => (
                  <FilePreview key={index}>
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
                    <DeleteButton
                      type="button"
                      onClick={() => handleRemoveFile(index)}
                    >
                      Remove
                    </DeleteButton>
                  </FilePreview>
                ))}
              </FileWrapper>
            </div>
          </div>
          <ButtonGroup>
            <Button
              type="button"
              onClick={() => {
                onClose();
                setNewPost(initPost);
                setFiles([]);
              }}
              color="secondary"
            >
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
