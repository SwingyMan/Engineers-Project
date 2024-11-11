import { useState } from "react";
import styled from "styled-components";
import { Button } from "../Button/Button";
import { useAuth } from "../../Router/AuthProvider";

const StyledForm = styled.form`
  width: 450px;
  display: flex;
  flex-direction: column;
  align-items: center;
  box-sizing: border-box;
  color: inherit;
`;
const StyledInput = styled.input`
  height: 100%;
  font-size: 1.2em;
  background-color: transparent;
  border: none;
  outline: none;
  width: 100%;
  padding: 0 10px;
  color: inherit;
`;

const InputWraper = styled.div`
  display: flex;
  width: 90%;
  height: 2.4em;
  margin: 5px;
  border: 1px solid var(--light-border);
  border-radius: 10px;
  background-color: var(--whiteTransparent20);
`;


export function RegisterForm() {
    const [input, setInput] = useState({
        Email: "",
        Username:"",
        Password: "",
      });
      const handleInput = (e: { target: { name: string; value: string } }) => {
        const { name, value } = e.target;
        setInput((prev) => ({
          ...prev,
          [name]: value,
        }));
      };
      
      const handleSubmitEvent = (e: { preventDefault: () => void }) => {
        e.preventDefault();

      };
  return (
    <StyledForm onSubmit={handleSubmitEvent}>
      <InputWraper>
        <StyledInput
          type="text"
          name="Email"
          placeholder="E-mail"
          onChange={handleInput}
          autoFocus={true} 
        />
      </InputWraper>
      <InputWraper>
        <StyledInput
          type="password"
          name="Password"
          placeholder="Password"
          onChange={handleInput}
        />
      </InputWraper>

      <Button onClick={() => {}} value={"Login"} />
    </StyledForm>
  );
}
