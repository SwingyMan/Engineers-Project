import { ImageDiv } from "../Utility/ImageDiv";
import { TimeElapsed } from "../../Utility/TimeElapsed";
import styled from "styled-components";
import { OptionMenu } from "../Utility/OptionMenu";
import { useNavigate } from "react-router";
import { getUserImg } from "../../API/API";
import { PostDTO } from "../../API/DTO/PostDTO";
import { ResolveAttachement } from "../../Utility/ResolveAttachement";
import { MdMoreHoriz } from "react-icons/md";
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
  color: var(--white);
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
const Options = styled.div`
  position: relative;
  cursor: pointer;
  height: 25px;
  transition: 150ms;
  &:hover {
    background-color: var(--whiteTransparent20);
  }
`;

const StyledMenu = styled.div`
  position: absolute;
  left: 50%;
  color: var(--offBlack);
  transform: translate(-50%);
  display: block;
`;

const OptionList = styled.div`
  padding: 10px;
  color: var(--offBlack);
  background-color: var(--white);
  &:hover {
    background-color: grey;
  }
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
export function Post(props: {
  postInfo: PostDTO;
  isMenu: boolean;
  isOpen: boolean;
  setIsOpen: Function;
}) {
  const CheckAtachments = (fileName: string) => {
    const ext = fileName.slice(fileName.lastIndexOf(".")).toLowerCase();
    if (imageExtensions.includes(ext)) return true;
    if (videoExtensions.includes(ext)) return true;
    return false
  };
  const ImageAttachments = props.postInfo.attachments?.filter(att=>CheckAtachments(att.realName))
  const FileAttachments = props.postInfo.attachments?.filter(att=>CheckAtachments(att.realName)===false)
  const navigate = useNavigate();
  const optionList = ["edit", "delete"];
  return (
    <PostWrapper>
      <PostHeader
        onClick={() => {
          navigate(`/post/${props.postInfo.id}`);
        }}
      >
        <HeaderInfo>
          <ImageDiv
            width={40}
            url={
              props.postInfo.avatarName
                ? getUserImg(props.postInfo.avatarName)
                : ""
            }
          />
          <div>
            <div
              onClick={(e) => {
                e.stopPropagation(),
                  navigate(`/profile/${props.postInfo.userId}`);
              }}
            >
              {props.postInfo.username}
            </div>
            <div>{TimeElapsed(props.postInfo.createdAt)}</div>
          </div>
        </HeaderInfo>
        {props.isMenu ? (
          <Options
            className="dropdown-container"
            onClick={(e) => {
              e.stopPropagation(), props.setIsOpen();
            }}
          >
            <MdMoreHoriz style={{ pointerEvents: "none" }} size={25} />
            {props.isOpen && (
              <StyledMenu>
                {optionList.map((option) => (
                  <OptionList
                    key={option}
                    data-action={option}
                  >
                    {option}
                  </OptionList>
                ))}
              </StyledMenu>
            )}
          </Options>
        ) : null}
      </PostHeader>
      <hr />
      <Title>{props.postInfo.title}</Title>
      <div>{props.postInfo.body}</div>
      {ImageAttachments &&
        ImageAttachments.length !== 0 && (
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
        {FileAttachments &&
        FileAttachments.length !== 0 && (
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
      <div>Komentarze ({props.postInfo.comments.length})</div>
    </PostWrapper>
  );
}
