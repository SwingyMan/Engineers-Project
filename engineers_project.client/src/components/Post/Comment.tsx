import styled from "styled-components";
import { ImageDiv } from "../Utility/ImageDiv";
import { useNavigate } from "react-router";
import { TimeElapsed } from "../../Utility/TimeElapsed";

const CommentWrapper = styled.div`
display: flex;
padding: 5px;
color: var(--white);
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
const Username = styled.div`
    font-weight: 400;
    cursor: pointer;
`
const CommentHeader = styled.div`
    display: flex;
`
const Date = styled.div`
    padding-left:3px ;
    font-weight: lighter;
`
export function Comment(props: { comment: CommentDTO }) {
    const navigate = useNavigate()
    return (
        <CommentWrapper>
            <ImageDiv width={30} url={props.comment.avararName ? props.comment.avararName : ""} margin=".5em 0 0 0"/>
            <ContentWrapper>
                <CommentHeader>
                <Username onClick={() => navigate(`/profile/${props.comment.userId}`)}>
                    
                        {props.comment.username}
                    
                </Username>
                <Date>
                • 
                {TimeElapsed(props.comment.createdDate)}
                </Date>
                </CommentHeader>
                <Comm>
                    {/* comment */}
                    {props.comment.content}
                </Comm>
            </ContentWrapper>
        </CommentWrapper>);
}