import { ChangeEvent, FocusEvent, useState } from "react";
import { MdSearch } from "react-icons/md";
import styled from "styled-components";
import { fetchPostByTitle } from "../../API/services/searchByName.service";
import { useNavigate } from "react-router";

//bacground color do zmiany
const StyledSearchBar = styled.div`
  width: 30%;
  color: white;
  font-size: 16px;
  z-index: 1;
`;
const SearchInput = styled.div<{ open: boolean }>`
  display: flex;
  background-color: rgb(69, 114, 159);
  height: 40px;
  border-radius: ${(props) =>
    props.open == true ? "20px" : "20px 20px 0px 0px"};
  margin: ${(props) => (props.open == true ? "5px auto" : "5px auto 0px auto")};
  box-sizing: border-box;
  color: white;
  align-items: center;
  padding-left: 5px;
  &:focus {
    border: solid 1px white;
  }
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
  & > :last-child {
    border-radius: 0 0 15px 15px;
  }
  & > :hover {
    background-color: #003c7d;
  }
`;
const ResultRow = styled.div`
  padding: 5px 15px;
  background-color: rgb(69, 114, 159);
  cursor: pointer;
  border-top: rgba(255, 255, 255, 0.3) 1px solid;
`;

export function SearchBar() {
  const [query, setQuery] = useState(""); // User input
  const init: PostDTO[] = [];
  const [results, setResults] = useState(init); // API results
  const [loading, setLoading] = useState(false); // Loading state
  const [active, setActive] = useState(false);
  // Function to handle API search
  const handleSearch = async (e: ChangeEvent<HTMLInputElement>) => {
    const input = e.target.value;
    console.log(input);
    setQuery(() => input);

    if (input.length < 2) {
      setResults([]);
      return;
    }

    setLoading(true);
    try {
      // Replace with your actual API endpoint
      const response = fetchPostByTitle(input);
      const data = await response;
      setResults(data); // Adjust based on API response structure
    } catch (error) {
      console.error("Error fetching data:", error);
    } finally {
      setLoading(false);
    }
  };
  const navigate = useNavigate();
  const handleBlur=(e: FocusEvent<HTMLDivElement, Element>)=>{
    if (!e.currentTarget.contains(e.relatedTarget)) {
      setActive(false);
  }
  }
  return (
    <StyledSearchBar tabIndex={-1}onBlur={(e)=>handleBlur(e)}>
      <form
      // onSubmit={(e) => (e.preventDefault(), navigate("/post/"))}
      >
        <SearchInput open={!active}>
          <MdSearch size={32} />
          <StyledInput
            onFocus={() => setActive(true)}
            
            name="query"
            value={query}
            onChange={(e) => handleSearch(e)}
          />
        </SearchInput>
      </form>
      <SearchResult>
        {/* search result */}
        { !active ? null : query.length < 2 ? (
          <ResultRow>Wpisz co najmniej 2 znaki</ResultRow>
        ) : loading ? (
          <ResultRow>Loading...</ResultRow>
        ) : results.length === 0 ? (
          <ResultRow>Not Found</ResultRow>
        ) : (
          results.slice(0, 7).map((row) => (
            <ResultRow
              onClick={() => {
                navigate("/post/" + row.id), 
                setActive(false)
              }}
              key={row.id}
            >
              {row.title}
            </ResultRow>
          ))
        )}
      </SearchResult>
    </StyledSearchBar>
  );
}
