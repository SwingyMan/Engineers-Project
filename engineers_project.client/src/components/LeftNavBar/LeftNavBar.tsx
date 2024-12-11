import { FaHome } from "react-icons/fa";
import { useNavigate } from "react-router";
import styled from "styled-components";

const NavBarWrapper = styled.div`
  display: flex;
  width: 15%;
  height: 100%;
  color: var(--white);
`;
const ButttonWrapper = styled.div<{ avtive?: boolean }>`
  display: flex;
  padding: 5px 10px;
  height: fit-content;
  width: 100%;
  align-content: center;
  place-items: center;
  background-color: gray;
  border-radius: 4px;
  min-width: 0px;
  margin: 10px;
`;
const MainNavMenu = styled.div`
display: flex;
flex-grow: 1;
`;

export function LeftNavBar() {
    const navigate = useNavigate()
  return (
    <NavBarWrapper>
      <MainNavMenu>
        <ButttonWrapper onClick={()=>navigate("/")}>
          <FaHome /> Home
        </ButttonWrapper>
      </MainNavMenu>
    </NavBarWrapper>
  );
}
