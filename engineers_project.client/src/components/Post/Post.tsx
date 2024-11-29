import { ImageDiv } from "../Utility/ImageDiv";
import { TimeElapsed } from "../../Utility/TimeElapsed";
import styled from "styled-components";
import { OptionMenu } from "../Utility/OptionMenu";
import { useNavigate } from "react-router";
import { Comment } from "./Comment";


const PostWrapper = styled.div`

  padding: 1em;
  background-color: #6085afcc;
  /*  */
  margin: 1em;
  border-radius: 10px;
  border: 1px solid var(--darkGrey);

`;
const PostHeader = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
`;
const HeaderInfo = styled.div`
    display: flex;
    gap:4px;
    cursor: pointer;
`
const Title = styled.div`
  font-size: 1.2em;
  font-weight: 400;
  margin-bottom: .1em;
`

export function Post(props: { postInfo: PostDTO, details:1|0 }) {
  const navigate = useNavigate()
  return (
    <PostWrapper>
      <PostHeader onClick={() => { navigate(`/post/${props.postInfo.id}`, { state: props.postInfo }) }}>
        <HeaderInfo>
          <ImageDiv width={40} url={props.postInfo.avatarFileName ? props.postInfo.avatarFileName : ""} />
          <div>
            <div onClick={(e) => { e.stopPropagation(), navigate(`/profile/${props.postInfo.userId}`) }}>{props.postInfo.username}</div>
            <div >
              {TimeElapsed(props.postInfo.createdAt)}
            </div>
          </div>
        </HeaderInfo>
        <OptionMenu />
      </PostHeader>
      <hr />
      <Title>{props.postInfo.title}</Title>
      <div><b>{props.postInfo.body}</b></div>
      <hr />
      {props.details===1?(props.postInfo.comments.map((comment)=>(<Comment comment={comment}/>))):
      (<div>
        Komentarze ({props.postInfo.comments.length})
      </div>)}
    </PostWrapper>
  );
}
