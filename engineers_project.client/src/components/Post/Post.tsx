import { ImageDiv } from "../Utility/ImageDiv";
import { TimeElapsed } from "../../Utility/TimeElapsed";
import styled from "styled-components";
import { OptionMenu } from "../Utility/OptionMenu";
import { useNavigate } from "react-router";
import { getImg } from "../../API/API";
import { PostDTO } from "../../API/DTO/PostDTO";


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
    gap:4px;
    cursor: pointer;
`
const Title = styled.div`
  font-size: 1.2em;
  font-weight: 400;
  margin-bottom: .1em;
`

export function Post(props: { postInfo: PostDTO,isMenu:boolean, isOpen:boolean,setIsOpen:Function}) {
  const navigate = useNavigate()


  return (
    <PostWrapper>
      <PostHeader onClick={() => { navigate(`/post/${props.postInfo.id}`, { state: props.postInfo }) }}>
        <HeaderInfo>
          <ImageDiv width={40} url={props.postInfo.avatarName ? getImg(props.postInfo.avatarName) : ""} />
          <div>
            <div onClick={(e) => { e.stopPropagation(), navigate(`/profile/${props.postInfo.userId}`) }}>{props.postInfo.username}</div>
            <div >
              {TimeElapsed(props.postInfo.createdAt)}
            </div>
          </div>
        </HeaderInfo>
        {props.isMenu?<OptionMenu id={props.postInfo.id} isOpen={props.isOpen} setIsOpen={props.setIsOpen}/>:null}
      </PostHeader>
      <hr />
      <Title>{props.postInfo.title}</Title>
      <div>{props.postInfo.body}</div>
      <hr />
     <div>
        Komentarze ({props.postInfo.comments.length})
      </div>
    </PostWrapper>
  );
}
