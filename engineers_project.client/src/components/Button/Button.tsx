import styled from "styled-components";

const StyledButton = styled.button`
  //Login only temp
  border-radius: 10px;
  background-color: rgba(0, 0, 0, 0.5);
  width: 40%;
  border: 1px solid var(--light-border);
  text-align: center;
  height: 3em;
  margin: 5px;
  font-size: 1em;
  padding: 1em;
  line-height: 1em;
  font-weight: bold;
  cursor: pointer;
  color: inherit;
`;
export function Button({value,onClick}:{value:string,onClick:Function}) {
  return <StyledButton onClick={()=>onClick()}>{value}</StyledButton>;
}
