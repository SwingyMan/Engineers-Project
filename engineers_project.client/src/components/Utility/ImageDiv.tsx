import styled from "styled-components";
const StyledImage = styled.div`
    height: 100%;
    display: flex;
    align-items: center;
`
 export function ImageDiv(props: { width: number; url: string | undefined; }){
    return<>
            <StyledImage> 
            <svg aria-hidden="true" width={props.width} height={props.width}>
            <mask id=":circ:">                
            <circle cx={props.width/2} cy={props.width/2} r={props.width/2} fill="white" ></circle>
            </mask>
            <g mask="url(#:circ:)">
                <image x="0" y="0" preserveAspectRatio="xMidYMid slice" height={"100%"} href={props.url} width={"100%"}
                />
                <circle stroke="grba(0,0,0,0.05)" fill="none" cx={props.width/2} cy={props.width/2} r={props.width/2} ></circle>
            </g>
            </svg>
            </StyledImage>  
    </>
}