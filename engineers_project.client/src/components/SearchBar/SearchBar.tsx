import { ChangeEvent, FocusEvent, useState } from "react";
import { MdSearch } from "react-icons/md";
import styled from "styled-components";
import { useNavigate } from "react-router";
import { useSearchPosts } from "../../API/hooks/useSearchPosts";
import { useSearchUsers } from "../../API/hooks/useSearchUsers";
import { UserCard } from "../User/UserCard";

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
  const [active, setActive] = useState(false);
  const { data: postData, isFetching, isError } = useSearchPosts(query);
  const { data: userData } = useSearchUsers(query);
  // Function to handle API search
  const handleSearch = async (e: ChangeEvent<HTMLInputElement>) => {
    const input = e.target.value;
    setQuery(() => input);
  };
  const navigate = useNavigate();
  const handleBlur = (e: FocusEvent<HTMLDivElement, Element>) => {
    if (!e.currentTarget.contains(e.relatedTarget)) {
      setActive(false);
    }
  };
  console.log(userData)
  return (
    <StyledSearchBar tabIndex={-1} onBlur={(e) => handleBlur(e)}>
      <form
        onSubmit={(e) => (
          e.preventDefault(),
          query.length < 2 ? null : navigate("/search/" + query)
        )}
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
        {!active ? null : query.length < 2 ? (
          <ResultRow>Wpisz co najmniej 2 znaki</ResultRow>
        ) : isFetching ? (
          <ResultRow>Loading...</ResultRow>
        ) : (postData && postData.length === 0 )&&(userData&&userData.length===0)? (
          <ResultRow>Not Found</ResultRow>
        ) : (
          <>
            {postData &&
              postData.slice(0, 7).map((row) => (
                <ResultRow
                  onClick={() => {
                    navigate("/post/" + row.id), setActive(false);
                  }}
                  key={row.id}
                >
                  {row.title}
                </ResultRow>
              ))}
            {userData&&userData.slice(0,7).map((row)=>
            <UserCard user={row}/>
            )}
          </>
        )}
      </SearchResult>
    </StyledSearchBar>
  );
}
