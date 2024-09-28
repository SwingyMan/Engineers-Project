import { MdSearch } from "react-icons/md";
import { styled } from "styled-components";

const StyledTopBar = styled.div`
  background-color: var(--blue);
  height: 50px;
  width: 100%;
  display: flex;
`;
const Logo = styled.div`
  margin: 5px;
  & > {
    width: 40px;
    height: 40px;
  }
`;
const SearchBar = styled.div`
  display: flex;
  background-color: rgba(234, 245, 241, 0.295);
  height: 40px;
  width: 30%;
  z-index: 3;
  border-radius: 30px;
  margin: 5px auto;
  box-sizing: border-box;
  color: white;
  align-items: center;
  padding-left:5px;
`;
const Input = styled.input`
  background-color: transparent;
  width: 100%;
  border: 0;
  color: inherit;
  &:focus-visible {
    outline: none;
  }
  &::placeholder{
    color: inherit;
  }
`;
export function TopNavBar() {
  return (
    <>
      <StyledTopBar>
        <Logo>
          <img height={40} width={40} src="src/assets/logo-politechnika.png" />
        </Logo>
        <SearchBar>
          <MdSearch size={32} />
          <Input spellCheck={false} placeholder="Szukaj"/>
        </SearchBar>
      </StyledTopBar>
    </>
  );
}
