import { Comments } from "../Comments/Comments";
import { ImageDiv } from "../Utility/ImageDiv";
import { TimeElapsed } from "../../Utility/TimeElapsed";
import styled from "styled-components";
import { MdMoreHoriz } from "react-icons/md";

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
`

export function Post(props: { postInfo: PostDTO }) {
  return (
    <PostWrapper>
      <PostHeader>
        <HeaderInfo>
          <ImageDiv width={40} url={props.postInfo.img} />
          <div>
            <div>{props.postInfo.name}</div>
            <div >
              {TimeElapsed(props.postInfo.dateOfCreation)}
            </div>
          </div>
        </HeaderInfo>
        <div>
            <MdMoreHoriz size={25}/>
        </div>
      </PostHeader>
      <hr />
      <div>{props.postInfo.content}</div>
      <hr />
      <Comments />
    </PostWrapper>
  );
}
