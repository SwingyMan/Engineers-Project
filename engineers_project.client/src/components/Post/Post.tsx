import { Comments } from "../Comments/Comments";
import { ImageDiv } from "../Utility/ImageDiv";
import { TimeElapsed } from "../../Utility/TimeElapsed";
import styled from "styled-components";
import { OptionMenu } from "../Utility/OptionMenu";
import { useNavigate } from "react-router";


const PostWrapper = styled.div`
  padding: 1em;
  background-color: #6085afcc;
  /*  */
  margin: 1em;
  border-radius: 10px;
  border: 1px solid var(--darkGrey);
  align-items: flex-start;
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

export function Post(props: { postInfo: PostDTO }) {
  const navigate= useNavigate()
  return (
    <PostWrapper>
      <PostHeader>
        <HeaderInfo onClick={()=>{navigate(`/profile/${props.postInfo.user.id}`)}}>
          <ImageDiv width={40} url={props.postInfo.user.avatarFileName}/>
          <div>
            <div>{props.postInfo.user.username}</div>
            <div >
              {TimeElapsed(props.postInfo.createdAt)}
            </div>
          </div>
        </HeaderInfo>
<OptionMenu/>
      </PostHeader>
      <hr />
      <div>{props.postInfo.body}</div>
      <hr />
      <Comments />
    </PostWrapper>
  );
}
