import { useState } from "react";
import { getAttachment } from "../API/API";

type MimeTypeMap = {
    [key: string]: string;
  };
  
  const mimeTypes: MimeTypeMap = {
    jpeg: 'image/jpeg',
    jpg: 'image/jpeg',
    png: 'image/png',
    apng: 'image/apng',
    mp4: 'video/mp4',
    webm: 'video/webm',
  };
  
  export const resolveAttachement = async (url:string) => {
    const [mediaSrc, setMediaSrc] = useState<string | null>(null);
    const [mediaType, setMediaType] = useState<string | null>(null);
    
    try {
        const response = await fetch(getAttachment(url));
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
  
        // Extract filename and file extension
        const contentDisposition = response.headers.get('Content-Disposition');
        let fileName: string | null = null;
        console.log(contentDisposition)
  
        if (contentDisposition) {
          const utf8Match = contentDisposition.match(/filename\*=UTF-8''(.+)/i);
          const standardMatch = contentDisposition.match(/filename="([^"]+)"/i) || contentDisposition.match(/filename=([^;]+)/i);
  
          if (utf8Match) {
            fileName = decodeURIComponent(utf8Match[1]);
          } else if (standardMatch) {
            fileName = standardMatch[1];
          }
        }
  
        fileName = fileName || 'unknown';
        const fileExtension = fileName.split('.').pop()?.toLowerCase();
  
        if (!fileExtension || !(fileExtension in mimeTypes)) {
          throw new Error(`Unsupported or missing file extension: ${fileExtension}`);
        }
  
        const mimeType = mimeTypes[fileExtension];
        setMediaType(mimeType);
  
        // Convert response to a Blob
        const blob = await response.blob();
  
        // Create an object URL for the media
        const objectUrl = URL.createObjectURL(blob);
        setMediaSrc(objectUrl);
  
        // Revoke URL after some time to free memory
        setTimeout(() => URL.revokeObjectURL(objectUrl), 30000);
      } catch (error) {
        console.error('Error fetching file:', error);
      }
    return({mediaSrc,mediaType})

  };