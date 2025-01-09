import { useEffect, useState } from "react";
import styled from "styled-components";
import { imageExtensions, videoExtensions } from "../../interface/FileTypes";
const StyledFile = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  max-width: 200px; /* Limit file block width */
  word-wrap: break-word; /* Handle long file names */
  padding: 4px;
  border:1px solid var(--white) ;
`
const AttachmentItem = styled.div`
  display: flex;
  flex-shrink: 0; /* Prevent shrinking */
  justify-content: center;
  align-items: center;
  max-width: 100%; /* Constrain max width */
  max-height: 300px; /* Constrain max height */
  overflow: hidden;

  img, video {
    max-width: 100%;
    max-height: 100%;
    object-fit: contain; /* Ensure images and videos fit within bounds */
  }
`;
export function FileViewer(props: {
    fileName:string, fileSize:number, fileContent:ArrayBuffer 
}) {
  const [fileUrl, setFileUrl] = useState<string>("");
    
  // Generate a Blob URL for the file

  useEffect(() => {
    const blob = new Blob([props.fileContent]);
    const url = URL.createObjectURL(blob);
    setFileUrl(url);

    return () => {
      URL.revokeObjectURL(url); // Clean up the URL when the component unmounts
    };
  }, [props.fileContent]);

  // Determine the file type based on the file extension
  const getFileType = (): string => {
    const ext = props.fileName
      .slice(props.fileName.lastIndexOf("."))
      .toLowerCase();
    

    if (imageExtensions.includes(ext)) return "image";
    if (videoExtensions.includes(ext)) return "video";
    return "other";
  };

  const fileType = getFileType();

  return (
    <AttachmentItem>
      {fileType === "image" && (
        <img src={fileUrl} alt={props.fileName} style={{ maxWidth: "100%" }} />
      )}
      {fileType === "video" && (
        <video controls style={{ maxWidth: "100%" }}>
          <source src={fileUrl} />
          Your browser does not support the video tag.
        </video>
      )}
      {fileType === "other" && (
        <StyledFile>
          <p>
            {props.fileName} ({(props.fileSize / 1024).toFixed(2)} KB)
          </p>
          <a href={fileUrl} download={props.fileName}>
            Download
          </a>
        </StyledFile>
      )}
    </AttachmentItem>
  );
}
