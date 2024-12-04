import { useEffect, useState } from "react";
import { Button } from "../Button/Button";
import styled from "styled-components";
import { useAuth } from "../../Router/AuthProvider";
import { replace, useNavigate } from "react-router-dom";
import { LoginForm } from "./LoginForm";
import { RegisterForm } from "./RegisterForm";

const LoginDiv = styled.div`
  display: flex;
  align-items: center;
  flex-direction: column;
  border-radius: 50px;
  /* border: 1px solid white; */
  margin-top: 10vh;
  padding: 10px;
  gap: 10px;
  color: var(--white);
  & > h1 {
    pointer-events: none;
  }
`;



export function Login() {
  const navigate = useNavigate();

  const { isAuthenticated } = useAuth();
  useEffect(() => {
    if (isAuthenticated) navigate("/",{replace:true});
  }, [isAuthenticated, navigate]);

  const [Login, setLogin] = useState("Login");
  const [Message, setMessage] = useState("Don't have an account?");
  const [otherAction, setOtherAction] = useState("Register");
  const size = 120;
  const msg = ["Don't have an account?", "Already have an account"];
  function update() {
    if (Message == msg[0]) {
      setLogin("Register");
      setOtherAction("Login");
      setMessage(msg[1]);
    } else {
      setLogin("Login");
      setMessage(msg[0]);
      setOtherAction("Register");
    }
  }
  return (
    //TODO możliwe że register trzeba dać na osobną stronę/formulaz jako osobny komponent
  
    <LoginDiv>
      <svg aria-hidden="true" width={size} height={size}>
        <image
          x="0"
          y="0"
          height={"100%"}
          href={"src/assets/logo-politechnika.png"}
          width={"100%"}
        />
      </svg>
      <h1>{Login}</h1>
      {Message == msg[0]?<LoginForm/>:<RegisterForm/>}

      <div>{Message}</div>
      <Button
        onClick={() => {
          update();
        }}
        value={otherAction}
      />
    </LoginDiv>
  );
}
