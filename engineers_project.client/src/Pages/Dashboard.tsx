import { Outlet } from "react-router";
import { TopNavBar } from "../components/TopNavBar/TopNavBar";
import styled from "styled-components";
import { LeftNavBar } from "../components/LeftNavBar/LeftNavBar";
import { RightNavBar } from "../components/RightNavBar/RightNavBar";

const StyledPage = styled.div`
  width: 100%;
  flex: 1;
  display: flex;
  
  overflow: auto;
`;
export default function Dashboard() {
  return (
    <>
      <>
        <TopNavBar />
        <StyledPage>
          
          <LeftNavBar/>
          <Outlet />
          <RightNavBar/>
        </StyledPage>
      </>
    </>
  );
}
