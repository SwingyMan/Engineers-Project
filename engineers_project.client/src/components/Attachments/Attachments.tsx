import { useEffect, useState } from "react";
import styled from "styled-components";
const StyledFile = styled.div`
    border-radius: 4px;
    border: 1px solid var(--white);
    display: flex;
    flex-direction: column;
    width: fit-content;
`
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
    const imageExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".svg"];
    const videoExtensions = [".mp4", ".webm", ".ogg", ".mov"];

    if (imageExtensions.includes(ext)) return "image";
    if (videoExtensions.includes(ext)) return "video";
    return "other";
  };

  const fileType = getFileType();

  return (
    <div>
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
    </div>
  );
}
