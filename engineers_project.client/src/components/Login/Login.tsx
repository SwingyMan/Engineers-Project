import { useEffect, useState } from "react";
import styles from "./Login.module.css";
import { Button } from "../Button/Button";
import styled from "styled-components";
import { useAuth } from "../../Router/AuthProvider";
import { useNavigate } from "react-router-dom";

const InputWraper = styled.div`
  width: 90%;
  height: 2.4em;
  margin: 5px;
  border: 1px solid var(--light-border);
  border-radius: 10px;
  background-color: #3b3b3b;
`;
const StyledInput = styled.input`
  height: 100%;
  font-size: 1.2em;
  background-color: transparent;
  border: none;
  outline: none;
  width: 100%;
  padding: 0 10px;
`;

export function Login() {
    
	const navigate = useNavigate();
    
  const {logIn ,isAuthenticated} = useAuth()
    useEffect(() => {
		if (isAuthenticated) navigate('/');
	}, [isAuthenticated, navigate]);
    const [input, setInput] = useState({
		Username: '',
		Password: '',
	});
  const [Login, setLogin] = useState("Login");
  const [Message, setMessage] = useState("Don't have an account");
  const [otherAction, setOtherAction] = useState("Register");
  let size = 120;
  function update() {
    let msg = ["Don't have an account", "Already have an account"];
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
  const handleInput = (e: { target: { name: string; value: string } }) => {
    const { name, value } = e.target;
    console.log( name,value)
    setInput((prev) => ({
        ...prev,
        [name]: value,
    }));
};
const handleSubmitEvent = (e: { preventDefault: () => void }) => {
    e.preventDefault();
    console.log( input)
    if (input.Username !== '' && input.Password !== '') {
        logIn(input);
    } else {
        console.log('Nie podano loginu lub hasła!');
    }
};
  return (
    <>
      <dialog open>
        <div id={styles.LoginForm}>
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
          <form
            onSubmit={handleSubmitEvent}
          >
            <InputWraper>
              <StyledInput type="text" name="Username" placeholder="Username" onChange={handleInput} autoFocus={true} />
            </InputWraper>

            {Login == "Register" ? (
              <div className={styles.input}>
                <input type="text" name="E-mail" placeholder="E-mail" />
              </div>
            ) : (
              <></>
            )}
            <InputWraper>
              <StyledInput
                type="password"
                name="Password"
                placeholder="Password" onChange={handleInput}
              />
            </InputWraper>

            <Button onClick={() => {}} value={Login} />
          </form>
          <div>{Message}</div>
          <Button
            onClick={() => {
              update();
            }}
            value={otherAction}
          />
        </div>
      </dialog>
    </>
  );
}

import { useEffect, useState } from "react";
import styles from "./Login.module.css";
import { Button } from "../Button/Button";
import styled from "styled-components";
import { useAuth } from "../../Router/AuthProvider";
import { useNavigate } from "react-router-dom";

const InputWraper = styled.div`
  width: 90%;
  height: 2.4em;
  margin: 5px;
  border: 1px solid var(--light-border);
  border-radius: 10px;
  background-color: #3b3b3b;
`;
const StyledInput = styled.input`
  height: 100%;
  font-size: 1.2em;
  background-color: transparent;
  border: none;
  outline: none;
  width: 100%;
  padding: 0 10px;
`;

export function Login() {
    
	const navigate = useNavigate();
    
  const {logIn ,isAuthenticated} = useAuth()
    useEffect(() => {
		if (isAuthenticated) navigate('/');
	}, [isAuthenticated, navigate]);
    const [input, setInput] = useState({
		Username: '',
		Password: '',
	});
  const [Login, setLogin] = useState("Login");
  const [Message, setMessage] = useState("Don't have an account?");
  const [otherAction, setOtherAction] = useState("Register");
  let size = 120;
  function update() {
    let msg = ["Don't have an account?", "Already have an account"];
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
  const handleInput = (e: { target: { name: string; value: string } }) => {
    const { name, value } = e.target;
    console.log( name,value)
    setInput((prev) => ({
        ...prev,
        [name]: value,
    }));
};
const handleSubmitEvent = (e: { preventDefault: () => void }) => {
    e.preventDefault();
    console.log( input)
    if (input.Username !== '' && input.Password !== '') {
        logIn(input);
    } else {
        console.log('Nie podano loginu lub hasła!');
    }
};
  return (
    <>
      <dialog open>
        <div id={styles.LoginForm}>
          <svg aria-hidden="true" width={size} height={size}>
            <image
              x="0"
              y="0"
              height={"100%"}
              href={"src/assets/polsl.png"}
              width={"100%"}
            />
          </svg>
          <h1>{Login}</h1>
          <form style={{textAlign:'center'}}
            onSubmit={handleSubmitEvent}
          >
            <InputWraper>
              <StyledInput type="text" name="Username" placeholder="Username" onChange={handleInput} autoFocus={true} />
            </InputWraper>

            {Login == "Register" ? (
              <div className={styles.input}>
                <input type="text" name="E-mail" placeholder="E-mail" />
              </div>
            ) : (
              <></>
            )}
            <InputWraper>
              <StyledInput
                type="password"
                name="Password"
                placeholder="Password" onChange={handleInput}
              />
            </InputWraper>
            
            <Button onClick={() => {}} value={Login} />
          </form>
          <div>{Message}</div>
          <Button
            onClick={() => {
              update();
            }}
            value={otherAction}
          />
        </div>
      </dialog>
    </>
  );
}
