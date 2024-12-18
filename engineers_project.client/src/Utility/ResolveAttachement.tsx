import { useEffect, useState } from "react";
import { getAttachment } from "../API/API";
import { FileViewer } from "../components/Attachments/Attachments";

type FileData = {
  fileName: string;
  fileSize: number; // Size in bytes
  fileContent: ArrayBuffer; // Binary data
};
export function ResolveAttachement  (props:{url: string, fileName: string}){
  const [fileData, setFileData] = useState<FileData | null>(null);

  useEffect(() => {
    // Fetch the binary data from the API
    const fetchFile = async () => {
      const response = await getAttachment(props.url);
      const fileContent = await response.arrayBuffer();
      const fileSize = fileContent.byteLength
      const fileName=props.fileName
      setFileData({ fileName, fileSize, fileContent });
    };
    fetchFile();
  }, []);

  return (
    <>
      {fileData ? (
        <FileViewer
          fileName={fileData.fileName}
          fileSize={fileData.fileSize}
          fileContent={fileData.fileContent}
        />
      ) : (
        <p>Loading...</p>
      )}
    </>
  );
};
