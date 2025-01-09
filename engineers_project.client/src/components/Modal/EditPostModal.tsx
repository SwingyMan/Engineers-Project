import React, { useEffect, useState } from "react";
import styled from "styled-components";
import { NewPost } from "../../API/DTO/NewPost";
import { usePostDetails } from "../../API/hooks/usePostDetails";

// import { getAttachment } from "../../API/API";

// Styled components (unchanged)
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
// const DeleteButton = styled.button`
//   background-color: #dd3d3d;
//   padding: 4px;
//   border-color: var(--whiteTransparent20);
//   border-radius: 4px;
// `;
// const FileWrapper = styled.div`
//   display: flex;
//   flex-wrap: nowrap;
//   gap: 1rem;
//   overflow-x: scroll;
//   &::-webkit-scrollbar {
//     height: 8px;
//   }

//   &::-webkit-scrollbar-thumb {
//     background: #ccc;
//     border-radius: 4px;
//   }

//   &::-webkit-scrollbar-track {
//     background: #f0f0f0;
//   }
// `;
// const FilePreview = styled.div`
//   border: 1px solid #ccc;
//   padding: 10px;
//   display: flex;
//   flex-direction: column;
//   word-break: break-all;
//   gap: 5px;
//   max-width: 150px;
//   text-overflow: clip;
//   text-align: center;
//   align-items: center;
// `;

// interface FileDetails {
//   file: File;
//   preview: string;
//   error: string | null;
// }

export function EditPostModal(props: {
  isOpen: boolean;
  onClose: () => void;
  initData?: string|null;
}) {
  if (!props.initData) return null;
  const { data,handleEditPost } = usePostDetails(props.initData);
  // const [files, setFiles] = useState<FileDetails[]>([]);
  // const MAX_FILE_SIZE = 2 * 1024 * 1024; // 2MB


  // const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
  //   const selectedFiles = event.target.files;
  //   if (!selectedFiles) return;

  //   const updatedFiles: FileDetails[] = Array.from(selectedFiles).map((file) => {
  //     const isFileTooLarge = file.size > MAX_FILE_SIZE;
  //     return {
  //       file,
  //       preview: !isFileTooLarge ? URL.createObjectURL(file) : "",
  //       error: isFileTooLarge
  //         ? `File "${file.name}" exceeds the 2MB size limit.`
  //         : null,
  //     };
  //   });

  //   // Avoid adding duplicates
  //   setFiles((prevFiles) => {
  //     const existingFileNames = new Set(prevFiles.map((f) => f.file.name));
  //     const filteredNewFiles = updatedFiles.filter(
  //       (newFile) => !existingFileNames.has(newFile.file.name)
  //     );
  //     return [...prevFiles, ...filteredNewFiles];
  //   });
  // };

  // const handleRemoveFile = (index: number) => {
  //   setFiles((prevFiles) => {
  //     const updatedFiles = [...prevFiles];
  //     // Revoke object URL to prevent memory leaks
  //     URL.revokeObjectURL(updatedFiles[index].preview);
  //     updatedFiles.splice(index, 1);
  //     return updatedFiles;
  //   });
  // };

  // useEffect(() => {
  //   const fetchFiles = async () => {
  //     try {
  //       for (let i = 0; i < props.initData.attachments?.length!; i++) {
  //         const attachment = props.initData.attachments![i];
  //         const res = await getAttachment(attachment.id);

  //         if (!res.ok) {
  //           throw new Error(`Error fetching file: ${res.statusText}`);
  //         }

  //         const blob = await res.blob();
  //         const file = new File([blob], attachment.fileName, { type: blob.type });
  //         const preview = URL.createObjectURL(file);

  //         const newFileDetails: FileDetails = {
  //           file,
  //           preview,
  //           error: null,
  //         };

  //         // Avoid adding duplicates
  //         setFiles((prevFiles) =>
  //           prevFiles.some((f) => f.file.name === newFileDetails.file.name)
  //             ? prevFiles
  //             : [...prevFiles, newFileDetails]
  //         );
  //       }
  //     } catch (error) {
  //       console.error(error);
  //     }
  //   };

  //   if (props.initData.attachments) {
  //     fetchFiles();
  //   }

  //   return () => {
  //     // Cleanup URLs to prevent memory leaks
  //     files.forEach((file) => URL.revokeObjectURL(file.preview));
  //   };
  // }, [props.initData.attachments]);
if(!data)return null;
  const [editedPost, setEditedPost] = useState<NewPost>({
    title: data.title,
    body: data.body,
    availability: data.availability,
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const editedPostData = {
      entity: editedPost,
      id:data.id
    };
    handleEditPost.mutate(editedPostData)
    props.onClose();
  };

  const handleChange = (
    e: React.ChangeEvent<
      HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement
    >
  ) => {
    const { name, value } = e.target;
    setEditedPost({
      ...editedPost,
      [name]: name === "availability" ? Number(value) : value,
    });
  };
  return (
    <>
      {data && (
        <Overlay
          onClick={() => {
            props.onClose();
          }}
        >
          <ModalContainer onClick={(e) => e.stopPropagation()}>
            <Header>Edit Post</Header>
            <Form onSubmit={handleSubmit}>
              <Input
                type="text"
                placeholder="Title"
                name="title"
                value={editedPost.title}
                onChange={handleChange}
                required
              />
              <TextArea
                placeholder="Content"
                value={editedPost.body}
                name="body"
                onChange={handleChange}
                required
              />
              {data.availability !== 2 && (
                <Select
                  value={editedPost.availability}
                  onChange={handleChange}
                  name="availability"
                >
                  <option value={0}>Public</option>
                  <option value={1}>Private</option>
                </Select>
              )}
              {/* <div>
            <label htmlFor="fileInput">Upload files (Max size: 2MB each):</label>
            <input
              type="file"
              id="fileInput"
              multiple
              accept=".png,.jpg,.jpeg"
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
          </div> */}
              <ButtonGroup>
                <Button type="button" onClick={props.onClose} color="secondary">
                  Cancel
                </Button>
                <Button type="submit" color="primary">
                  Submit
                </Button>
              </ButtonGroup>
            </Form>
          </ModalContainer>
        </Overlay>
      )}{" "}
    </>
  );
}
