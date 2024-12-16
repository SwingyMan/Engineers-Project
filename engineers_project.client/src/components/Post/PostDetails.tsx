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


const PostWrapper = styled.div`
  padding: 1em;
  background-color: #6085afcc;
  /*  */
  margin: 1em;
  border-radius: 10px;
  border: 1px solid var(--darkGrey);
  height: min-content;
  min-width: 50%;
  width: clamp(50%, 60%, 70%);
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

export function PostDetails(props: {
  postInfo: PostDTO;
  options: boolean;
  isOpen: boolean;
  setIsOpen: Function;
}) {
  const navigate = useNavigate();
  const { data } = useComments(props.postInfo.id);

  return (
    <PostWrapper>
      <PostHeader>
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
      {props.postInfo.attachments &&
        props.postInfo.attachments.length !== 0 &&
        props.postInfo.attachments.map((att) => (<ResolveAttachement url={att.id} fileName={att.fileName} />
        ))}
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
