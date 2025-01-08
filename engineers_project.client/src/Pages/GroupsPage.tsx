import styled from "styled-components";
import { useGroups } from "../API/hooks/useGroups";
import { GroupCard } from "../components/Group/GroupCard";

const GroupsFeed = styled.div`
  flex: 1;
  overflow-y: scroll;
  color: var(--white);
`;
const SearchResult = styled.div`
  display: flex;
  justify-content: center;
  font-size: 1.5em;
`;
const GroupResultItems = styled.div`
  max-height: 60vh;
  overflow-y: scroll;
  width: 97%;
`;
const GroupResult = styled.div`
  max-height: 100%;
`;
export function GroupsPage() {
    const { data, isFetched, isError } = useGroups()
    return (
        <GroupsFeed>
            {isError?<>An error has occured</>:!isFetched ? <>Loading</> :
                data && data?.length !== 0 ? (
                    <GroupResult>
                        <GroupResultItems>
                            {data.map((group) => (
                                <GroupCard key={group.id} group={group} />
                            ))}
                        </GroupResultItems>
                    </GroupResult>
                ) : (
                    <SearchResult>No Groups Found</SearchResult>
                )}
        </GroupsFeed>
    )
}