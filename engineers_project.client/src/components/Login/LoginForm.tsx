import { useState } from "react";
import styled from "styled-components";
import { Button } from "../Button/Button";
import { useAuth } from "../../Router/AuthProvider";
import { VscEye, VscEyeClosed } from "react-icons/vsc";

const StyledForm = styled.form`
  width: 450px;
  display: flex;
  flex-direction: column;
  align-items: center;
  box-sizing: border-box;
  color: var(--white);
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
  position: relative;
`;
const EyeIcon = styled.div`
  position: absolute;
  right: 0px;
  padding-right: 12px;
  top: 4px;
  cursor: pointer;
  font-size: 22px;
  color: var(--white);
  &:hover{
    color: var(--whiteTransparent20);
    transition: 0.1s ease-in;
  }

`

export function LoginForm() {
    const [input, setInput] = useState({
        Email: "",
        Password: "",
      });
      
      const [visible,setVisible]= useState(false)
      const handleInput = (e: { target: { name: string; value: string } }) => {
        const { name, value } = e.target;
        setInput((prev) => ({
          ...prev,
          [name]: value,
        }));
      };
      
      const { logIn } = useAuth();
      const handleSubmitEvent = (e: { preventDefault: () => void }) => {
        e.preventDefault();
        logIn(input);
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
          type={visible?"text":"password"}
          name="Password"
          placeholder="Password"
          onChange={handleInput}
        />
                <EyeIcon onClick={()=>setVisible(!visible)}>
        {visible?<VscEye/>:<VscEyeClosed/>}
      </EyeIcon>
      </InputWraper>

      <Button onClick={() => {}} value={"Login"} />
    </StyledForm>
  );
}
