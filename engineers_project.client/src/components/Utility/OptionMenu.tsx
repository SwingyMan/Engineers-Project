import { useState } from "react";
import { MdMoreHoriz } from "react-icons/md";
import styled from "styled-components";

const Options = styled.div`
  position: relative;
  cursor: pointer;
  height: 25px;
  transition: 150ms;
  &:hover{
    
    background-color: var(--whiteTransparent20);
}
`;

const StyledMenu = styled.div<{open:boolean}>`
  position: absolute;
  left:50%;
  
  transform: translate(-50%);
  display: ${(props)=>props.open?'block':'none'};

`

const OptionList = styled.div`
padding:10px;

background-color: var(--white);
&:hover{
    background-color: var(--whiteTransparent20);
}
`;

const optionList = ["edit", "delete"];
export function OptionMenu() {
    const [open,setOpen] = useState(false)
  return (
    <Options onClick={(e)=>{e.stopPropagation();setOpen(!open)}}>
      <MdMoreHoriz size={25} />
      <StyledMenu open={open}>
        {optionList.map((option) => (
          <OptionList key={option}>{option}</OptionList>
        ))}
      </StyledMenu>
    </Options>
  );
}
