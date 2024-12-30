import { ImageDiv } from "../Utility/ImageDiv";
import { TimeElapsed } from "../../Utility/TimeElapsed";
import styled from "styled-components";
import { OptionMenu } from "../Utility/OptionMenu";
import { useNavigate } from "react-router";
import { Comment } from "./Comment";
import { CreateComment } from "./CreateComment";
import { getUserImg } from "../../API/API";
import { useComments } from "../../API/hooks/useComments";
import { PostDTO } from "../../API/DTO/PostDTO";
import { ResolveAttachement } from "../../Utility/ResolveAttachement";
import { imageExtensions, videoExtensions } from "../../interface/FileTypes";

const PostWrapper = styled.div`
  padding: 1em;
  background-color: #6085afcc;
  /*  */
  margin: 1em;
  border-radius: 10px;
  border: 1px solid var(--darkGrey);
  height: min-content;
  min-width: 50%;
  width: 75%;
`;
const PostHeader = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
`;
const HeaderInfo = styled.div`
  display: flex;
  gap: 4px;
  cursor: pointer;
`;
const Title = styled.div`
  font-size: 1.2em;
  font-weight: 400;
  margin-bottom: 0.1em;
`;
const AttachmentContainer = styled.div`
  display: flex;
  flex-wrap: nowrap; /* Prevent wrapping */
  overflow-x: auto; /* Add horizontal scrolling */
  gap: 16px; /* Add some spacing between attachments */
  max-width: 100%; /* Limit width */
  padding: 8px;

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
const FileContainer = styled.div`
  display: flex;
  flex-wrap: wrap; /* Prevent wrapping */
  overflow-x: auto; /* Add horizontal scrolling */
  gap: 16px; /* Add some spacing between attachments */
  max-width: 100%; /* Limit width */
  padding: 8px;
`;
export function PostDetails(props: {
  postInfo: PostDTO;
  options: boolean;
  isOpen: boolean;
  setIsOpen: Function;
}) {
  const navigate = useNavigate();
  const { data } = useComments(props.postInfo.id);
  const CheckAtachments = (fileName: string) => {
    const ext = fileName.slice(fileName.lastIndexOf(".")).toLowerCase();
    if (imageExtensions.includes(ext)) return true;
    if (videoExtensions.includes(ext)) return true;
    return false;
  };
  const ImageAttachments = props.postInfo.attachments?.filter((att) =>
    CheckAtachments(att.realName)
  );
  const FileAttachments = props.postInfo.attachments?.filter(
    (att) => CheckAtachments(att.realName) === false
  );

  return (
    <PostWrapper>
      <PostHeader>
        <HeaderInfo
          onClick={(e) => {
            e.stopPropagation(), navigate(`/profile/${props.postInfo.userId}`);
          }}
        >
          <ImageDiv
            width={40}
            url={
              props.postInfo.avatarName
                ? getUserImg(props.postInfo.avatarName)
                : ""
            }
          />
          <div>
            <div>{props.postInfo.username}</div>
            <div>{TimeElapsed(props.postInfo.createdAt)}</div>
          </div>
        </HeaderInfo>
        {props.options ? (
          <OptionMenu
            id={props.postInfo.id}
            isOpen={props.isOpen}
            setIsOpen={props.setIsOpen}
          />
        ) : null}
      </PostHeader>
      <hr />
      <Title>{props.postInfo.title}</Title>
      <div>{props.postInfo.body}</div>
      {ImageAttachments && ImageAttachments.length !== 0 && (
        <AttachmentContainer>
          {ImageAttachments.map((att) => (
            <ResolveAttachement
              key={att.fileName}
              url={att.id}
              fileName={att.realName}
            />
          ))}
        </AttachmentContainer>
      )}
      {FileAttachments && FileAttachments.length !== 0 && (
        <FileContainer>
          {FileAttachments.map((att) => (
            <ResolveAttachement
              key={att.fileName}
              url={att.id}
              fileName={att.realName}
            />
          ))}
        </FileContainer>
      )}
      <hr />
      <>
        <CreateComment id={props.postInfo.id} />

        {data
          ? data.map((comments) => (
              <Comment key={comments.id} comment={comments} />
            ))
          : props.postInfo.comments.map((comment) => (
              <Comment key={comment.id} comment={comment} />
            ))}
      </>
    </PostWrapper>
  );
}
