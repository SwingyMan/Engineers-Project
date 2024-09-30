import { styled } from "styled-components";
import { SearchBar } from "../SearchBar/SearchBar";

const StyledTopBar = styled.div`
  background-color: var(--blue);
  height: 50px;
  width: 100%;
  display: flex;
  justify-content:center;
`;
const Logo = styled.div`
  margin: 5px;
  position: absolute;
  left:0;
  & > {
    width: 40px;
    height: 40px;
  }
`;
const table = [
  {"id": "1", "value": "Apple"},
  {"id": "2", "value": "Banana"},
  {"id": "3", "value": "Cherry"},
  {"id": "4", "value": "Date"},
  {"id": "5", "value": "Elderberry"},
  {"id": "6", "value": "Fig"},
  {"id": "7", "value": "Grape"},
  {"id": "8", "value": "Honeydew"},
  {"id": "9", "value": "Kiwi"},
  {"id": "10", "value": "Lemon"}
]
function filter(searchInput:string){
  return(table.filter(r=> r.value.includes(searchInput)))

}
export function TopNavBar() {
  return (
    <>
      <StyledTopBar>
        <Logo>
          <img height={40} width={40} src="src/assets/logo-politechnika.png" />
        </Logo>
        <SearchBar searchFunction={filter}/>
      </StyledTopBar>
    </>
  );
}
