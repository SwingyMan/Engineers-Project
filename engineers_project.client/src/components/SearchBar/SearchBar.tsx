import { useEffect, useState } from "react";
import { MdSearch } from "react-icons/md";
import styled from "styled-components";

//bacground color do zmiany
const StyledSearchBar = styled.div`
  width: 30%;
  color:white;
  font-size: 16px;
  z-index: 1;
`
const SearchInput = styled.div<{open:boolean}>`
  display: flex;
  background-color: rgb(69, 114, 159);
  height: 40px;
  border-radius: ${(props)=>props.open==true?"20px":"20px 20px 0px 0px"};
  margin: ${(props)=>props.open==true?"5px auto":"5px auto 0px auto"};
  box-sizing: border-box;
  color: white;
  align-items: center;
  padding-left: 5px;
  
  `;
const StyledInput = styled.input`
  background-color: transparent;
  width: 100%;
  font-size: 14px;
  border: 0;
  color: inherit;
  &:focus-visible {
    outline: none;
  }
  &::placeholder {
    color: inherit;
  }
`;
const SearchResult = styled.div`
&>:last-child{
  border-radius: 0 0 15px 15px;
}
&>:hover{
    background-color: #003c7d;
  }
`;
const ResultRow = styled.div`
  padding:5px 15px;
  background-color: rgb(69, 114, 159);
  cursor: pointer;
border-top: rgba(255, 255, 255, 0.30) 1px solid;
`
interface row {
  id: string;
  value: string;
}

interface searchBar {
  searchFunction: (a: string) => { id: string; value: string }[];
}
export function SearchBar({ searchFunction }: searchBar) {
  const [searchValue, setSearchValue] = useState("");
  const init: row[] = [];
  const [searchResult, setSearchResult] = useState(init);
  useEffect(() => {
    if (searchValue.length >= 2) {
      setSearchResult(searchFunction(searchValue));
    }
    else {
      setSearchResult([]);
    }
  }, [searchValue]);
  return (
    <StyledSearchBar>
      <SearchInput open={(searchValue.length==0)}>
        <MdSearch size={32} />
        <StyledInput value={searchValue} onChange={(e) => setSearchValue(e.target.value)} />
      </SearchInput>
      <SearchResult>
        {/* search result */}
        {searchValue.length == 0 ?
          null : (
            searchValue.length < 2 ?
              <ResultRow>Wpisz co najmniej 2 znaki</ResultRow> :

              (searchResult.length > 0 ?
                (
                  searchResult.map(
                    (row) => <ResultRow key={row.id}>{row.value}</ResultRow> //row probabaly new element
                  )
                ) : (
                  <ResultRow>Nie znaleziono </ResultRow>
                ))
          )}
      </SearchResult>
    </StyledSearchBar>
  );
}
