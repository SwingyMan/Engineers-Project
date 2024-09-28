import { useEffect, useState } from "react";
import { MdSearch } from "react-icons/md";
import styled from "styled-components";

const StyledBar = styled.div`
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
  padding-left: 5px;
`;
const StyledInput = styled.input`
  background-color: transparent;
  width: 100%;
  border: 0;
  color: inherit;
  &:focus-visible {
    outline: none;
  }
  &::placeholder {
    color: inherit;
  }
`;
interface row {
  id: string;
  value: string;
}
function a(b: string) {
  const table: row[] = [];
  return table;
}
interface searchBar {
  searchFunction: (a: string) => { id: string; value: string }[];
}
export function SearchBar({ searchFunction }: searchBar) {
  const [value, setValue] = useState("");
  const init: row[] = [];
  const [searchResult, setSearchResult] = useState(init);
  useEffect(() => {
    if (value.length > 2) {
      setSearchResult(searchFunction(value));
    } else if(value.length<=2 && searchResult.length>0) {
      setSearchResult([]);
    }
  }, [value]);
  return (
    <div>
      <StyledBar>
        <MdSearch size={32} />
        <StyledInput value={value} onChange={(e) => setValue(e.target.value)} />
      </StyledBar>
      <div>
        {/* search result */}
        {searchResult.length > 0 ? (
          searchResult.map(
            (row) => <div key={row.id}>{row.value}</div> //row probabaly new element
          )
        ) : (
          <div>Wpisz co najmniej 2 znaki</div>
        )}
      </div>
    </div>
  );
}
