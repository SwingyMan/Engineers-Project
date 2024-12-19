import { useParams } from "react-router";
import { styled } from "styled-components";
import { useSearchPosts } from "../API/hooks/useSearchPosts";
import { Post } from "../components/Post/Post";
import { useAuth } from "../Router/AuthProvider";
import { useReducer, useState } from "react";
import { IoIosArrowDown, IoIosArrowUp } from "react-icons/io";
import { useSearchUsers } from "../API/hooks/useSearchUsers";
import { UserCard } from "../components/User/UserCard";
import { useSearchGroups } from "../API/hooks/useSearchGroups";
import { GroupCard } from "../components/Group/GroupCard";

const SearchFeed = styled.div`
  flex: 1;
  overflow-y: scroll;
  color: var(--white);
`;
const SearchResult = styled.div`
  display: flex;
  justify-content: center;
  font-size: 1.5em;
`;
const GroupResult = styled.div`
  max-height: 100%;
`;
const GroupResultHeader = styled.div`
  padding: 1em;
  background-color: #6085afcc;
  /*  */
  margin: 1em;
  border-radius: 10px;
  border: 1px solid var(--darkGrey);
  height: min-content;
  min-width: 50%;
  display: flex;
  align-items: center;
  justify-content: space-between;
`;
const GroupResultItems = styled.div`
  max-height: 60vh;
  overflow-y: scroll;
  width: 97%;
`;

function reducer(
  state: { groupMenu: any; userMenu: any; postMenu: any },
  action: { type: any }
) {
  switch (action.type) {
    case "TOGGLE_GROUPMENU":
      return { ...state, groupMenu: !state.groupMenu };
    case "TOGGLE_USERMENU":
      return { ...state, userMenu: !state.userMenu };
    case "TOGGLE_POSTMENU":
      return { ...state, postMenu: !state.postMenu };
    default:
      throw new Error(`Unknown action type: ${action.type}`);
  }
}
export function SearchPage() {
  const { query } = useParams();
  const handleMenuOpen = (id: string) => {
    setOpenMenu(id);
  };
  const [state, dispatch] = useReducer(reducer, {
    groupMenu: true,
    userMenu: true,
    postMenu: true,
  });
  const [openMenu, setOpenMenu] = useState<null | string>(null);

  const { user } = useAuth();
  const { data: PostData, isPending: isPostPending } = useSearchPosts(query!);
  const { data: UserData, isPending: isUserPending } = useSearchUsers(query!);
  
  const { data: GroupData, isPending: isGroupPending } = useSearchGroups(query!);
  return (
    <SearchFeed>
      <SearchResult> Search results for "{query}" </SearchResult>
      {isPostPending ? "Loading..." : null}
      {PostData && PostData?.length !== 0 ? (
        <GroupResult>
          <GroupResultHeader
            onClick={() => dispatch({ type: "TOGGLE_POSTMENU" })}
          >
            Posts
            {state.postMenu ? <IoIosArrowUp /> : <IoIosArrowDown />}
          </GroupResultHeader>
          {state.postMenu ? (
            <GroupResultItems>
              {PostData.map((post) => (
                <Post
                  key={post.id}
                  postInfo={post}
                  isMenu={post.userId === user?.id}
                  isOpen={openMenu === post.id}
                  setIsOpen={() => handleMenuOpen(post.id)}
                />
              ))}
            </GroupResultItems>
          ) : null}
        </GroupResult>
      ) : (
        <SearchResult>No Posts Found</SearchResult>
      )}

      {UserData && UserData?.length !== 0 ? (
        <GroupResult>
          <GroupResultHeader
            onClick={() => dispatch({ type: "TOGGLE_USERMENU" })}
          >
            Users
            {state.userMenu ? <IoIosArrowUp /> : <IoIosArrowDown />}
          </GroupResultHeader>
          {state.userMenu ? (
            <GroupResultItems>
              {UserData.map((user) => (
                <UserCard key={user.id} user={user} />
              ))}
            </GroupResultItems>
          ) : null}
        </GroupResult>
      ) : (
        <SearchResult>No Users Found</SearchResult>
      )}
      {GroupData && GroupData?.length !== 0 ? (
        <GroupResult>
          <GroupResultHeader
            onClick={() => dispatch({ type: "TOGGLE_GROUPMENU" })}
          >
            Groups
            {state.groupMenu ? <IoIosArrowUp /> : <IoIosArrowDown />}
          </GroupResultHeader>
          {state.groupMenu ? (
            <GroupResultItems>
              {GroupData.map((group) => (
                <GroupCard key={group.id} group={group} />
              ))}
            </GroupResultItems>
          ) : null}
        </GroupResult>
      ) : (
        <SearchResult>No Groups Found</SearchResult>
      )}
    </SearchFeed>
  );
}
 