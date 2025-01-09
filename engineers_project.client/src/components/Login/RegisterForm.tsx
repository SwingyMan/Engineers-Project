import { useState } from "react";
import styled from "styled-components";
import { Button } from "../Button/Button";
import { VscEye, VscEyeClosed } from "react-icons/vsc";
import { useMutation } from "@tanstack/react-query";
import { register } from "../../API/services/user.service";
import { UserDTO } from "../../API/DTO/UserDTO";

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
  &:user-invalid {
    border-radius: 10px;
    border: 2px solid red;
    box-shadow: 0px 0px 1px 1px red;
  }

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
  &:hover {
    color: var(--whiteTransparent20);
    transition: 0.1s ease-in;
  }
`;
const handleRegister = () => {
  return useMutation({
    mutationFn: async (user: UserDTO) => {
      return await register(user);
    },
    onSuccess: () => {
      alert("Activate account via e-mail");
    },
    onError: (e) => {
      alert(e);
    },
  });
};

export function RegisterForm() {
  const [input, setInput] = useState({
    Email: "",
    Username: "",
    Password: "",
  });
  const register = handleRegister();
  const [visible, setVisible] = useState(false);
  const handleInput = (e: { target: { name: string; value: string } }) => {
    const { name, value } = e.target;
    setInput((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const handleSubmitEvent = (e: { preventDefault: () => void }) => {
    register.mutate(input);
    e.preventDefault();
  };
  return (
    <StyledForm onSubmit={handleSubmitEvent}>
      <InputWraper>
        <StyledInput
          type="email"
          name="Email"
          pattern="^[^\s@]+@(polsl\.pl|student\.polsl\.pl)$"
          title="e-mail must be in a polsl.pl domain"
          placeholder="E-mail"
          onChange={handleInput}
          autoFocus={true}
          required
        />
      </InputWraper>
      <InputWraper>
        <StyledInput
          type="text"
          name="Username"
          pattern="^(.*?[A-Za-z\d]){4,}.*$"
          title="Username must be at least 4 characters long"
          required
          placeholder="Username"
          onChange={handleInput}
        />
      </InputWraper>
      <InputWraper>
        <StyledInput
          type={visible ? "text" : "password"}
          name="Password"
          required
          title="Password must be at least 8 characters long and include uppercase, lowercase, number, and special character."
          pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
          placeholder="Password"
          onChange={handleInput}
        />
        <EyeIcon onClick={() => setVisible(!visible)}>
          {visible ? <VscEye /> : <VscEyeClosed />}
        </EyeIcon>
      </InputWraper>

      <Button onClick={() => {}} value={"Register"} />
    </StyledForm>
  );
}
