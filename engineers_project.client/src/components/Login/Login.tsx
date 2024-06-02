import { useState } from "react";
import styles from './Login.module.css'


export function Login(){
    const [Login, setLogin] = useState("Login")
    const [Message,setMessage] = useState("Don't have an account")
    const [otherAction,setOtherAction]=useState("Register")
    let size = 120;
    function update(){
    let msg =["Don't have an account","Already have an account"]
    if (Message == msg[0]){
        setLogin("Register")
        setOtherAction("Login")
        setMessage(msg[1])
    }
    else{
        setLogin("Login")
        setMessage(msg[0])        
        setOtherAction("Register")
    }
    }
    return<>
    <dialog open>
    <div id={styles.LoginForm}>

        <svg aria-hidden="true" width={size} height={size}>
            <image x="0" y="0" height={"100%"} href={"src/assets/logo-politechnika.png"} width={"100%"}/>
        </svg>
        <h1>{Login}</h1>
        <div className={styles.input}>
            <input type="text" name="E-mail" placeholder="E-mail"/>
        </div>
        {Login=="Register"?
            <div className={styles.input}>
                <input type="text" name="Username" placeholder="Username"/>
            </div>:<></>
        }
        <div className={styles.input}>
            <input type="password" name="Password" placeholder="Password"/>
        </div>  
        <div className={styles.loginButton} id="Login"onClick={()=>{}}>
            {Login}
        </div>
        <div>{Message}</div>
        <div className={styles.loginButton} id="Register" onClick={()=>{update()}}>
            {otherAction}
        </div>


    </div>
    </dialog>
    
    
    
    
    </>
}