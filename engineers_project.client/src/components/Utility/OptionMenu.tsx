import { MdMoreHoriz } from "react-icons/md";
import styled from "styled-components";

const Options = styled.div`
  position: relative;
  cursor: pointer;
  height: 25px;
  transition: 150ms;
  &:hover {
    background-color: var(--whiteTransparent20);
  }
`;

const StyledMenu = styled.div`
  position: absolute;
  left: 50%;
  color: var(--offBlack);
  transform: translate(-50%);
  display: block;
`;

const OptionList = styled.div`
  padding: 10px;
  color: var(--offBlack);
  background-color: var(--white);
  &:hover {
    background-color: grey;
  }
`;

const optionList = ["edit", "delete"];
export function OptionMenu({
  id,
  isOpen,
  setIsOpen,
}: {
  id: string;
  isOpen: boolean;
  setIsOpen: Function;
}) {
  return (
    <Options
      className="dropdown-container"
      onClick={(e) => {
        e.stopPropagation(), setIsOpen();
      }}
    >
      <MdMoreHoriz size={25} />
      {isOpen && (
        <StyledMenu>
          {optionList.map((option) => (
            <OptionList key={option} id={id} dara-action>
              {option}
            </OptionList>
          ))}
        </StyledMenu>
      )}
    </Options>
  );
}
