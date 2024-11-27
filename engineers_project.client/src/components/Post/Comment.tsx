import styled from "styled-components";
import { ImageDiv } from "../Utility/ImageDiv";
import { useNavigate } from "react-router";

const CommentWrapper = styled.div`
display: flex;
padding: 5px;
`
const ContentWrapper = styled.div`
    padding:0 5px;
`
const Comm = styled.div`
margin-top: .25em;
    border-radius: 4px;
    background-color: var(--whiteTransparent20);
    padding: 5px;
`
export function Comment(props: { comment: CommentDTO }) {
    const navigate = useNavigate()
    return (
        <CommentWrapper>
            <ImageDiv width={30} url={props.comment.avararName ? props.comment.avararName : ""} />
            <ContentWrapper>
                <div onClick={() => navigate(`/profile/${props.comment.userId}`)}>
                    {/* username */}
                    <b>
                        {props.comment.username}
                    </b>
                </div>
                <Comm>
                    {/* comment */}
                    {props.comment.content}
                </Comm>
            </ContentWrapper>
        </CommentWrapper>);
}