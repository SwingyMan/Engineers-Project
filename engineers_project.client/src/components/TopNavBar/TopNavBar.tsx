import { styled } from "styled-components";
import styles from "../TopNavBar/TopNavBar.module.css";

const StyledTopBar = styled.div`

  background-color: var(--blue);
  height: 50px;
  width: 100%;
  display: flex;
`;
export function TopNavBar() {
  return (
    <>
      <StyledTopBar>
        <div id={styles.Logo}>
          <img height={40} width={40} src="src/assets/logo-politechnika.png" />
        </div>
        <div id={styles.SearchBar}>asdvasfa</div>
      </StyledTopBar>
    </>
  );
}
