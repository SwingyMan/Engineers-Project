import { Outlet } from "react-router";
import { TopNavBar } from "../components/TopNavBar/TopNavBar";



export default function Dashboard() {
  return (
    <>
      <>
        <TopNavBar />
        <Outlet />

      </>
    </>
  );
}
