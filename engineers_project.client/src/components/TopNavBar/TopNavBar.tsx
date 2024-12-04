import { styled } from "styled-components";
import { SearchBar } from "../SearchBar/SearchBar";
import { ImageDiv } from "../Utility/ImageDiv";
import { useAuth } from "../../Router/AuthProvider";
import { IoExitOutline } from "react-icons/io5";
import { getImg } from "../../API/API";

const StyledTopBar = styled.div`
  background-color: var(--blue);
  height: 50px;
  width: 100%;
  display: flex;
  justify-content: space-between;
  padding: 0 15px;
`;
const Logo = styled.div`
  margin: 5px;
  display: flex;
  & > {
    width: 40px;
    height: 40px;
  }
`;

const Buttons = styled.div`
  display: flex;
  gap: 10px;
`;
const LogoutButton = styled.button`
  padding: 10px;
  margin: 5px;
  display: flex;
  align-items: center;
  gap: 5px;
  background-color: inherit;
  border: 1px var(--whiteTransparent90) solid;
  color: white;
  border-radius: 4px;
  cursor: pointer;
  &:hover {
    background-color: var(--whiteTransparent20);
    transition: ease-out;
    transition-duration: 0.15s;
  }
`;


export function TopNavBar() {
  const { logOut, user } = useAuth();
  return (
    <>
      <StyledTopBar>
        <Logo>
          <img height={40} width={40} src="src/assets/logo-politechnika.png" />
        </Logo>
        <SearchBar  />
        <Buttons>
          <LogoutButton
            onClick={() => {
              logOut();
            }}
          >
            {"Wyloguj "}
            <IoExitOutline />
          </LogoutButton>
          <ImageDiv
            width={40}
            url={user?.avatarName ? `${getImg(user.avatarName)}` : ""}
          />
        </Buttons>
      </StyledTopBar>
    </>
  );
}
