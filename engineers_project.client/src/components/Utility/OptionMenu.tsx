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
  color: var(--offBlack);
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
export function OptionMenu({id,isOpen,setIsOpen}:{id:string,isOpen:boolean,setIsOpen:Function}) {

  return (
    <Options  onClick={(e)=>{e.stopPropagation(),setIsOpen()}}>
      <MdMoreHoriz size={25} />
      <StyledMenu open={isOpen}>
        {optionList.map((option) => (
          <OptionList key={option}>{option}</OptionList>
        ))}
      </StyledMenu>
    </Options>
  );
}
 