import { createContext, useContext, useState } from "react";
import { Children } from "../interface/Children";
import { UserContextProps } from "../interface/UserContextProps";
import { UserDTO } from "../API/DTO/UserDTO";
import { User } from "../API/DTO/User";
import { fetchUserById } from "../API/services/user.service";
import {getHost} from "../API/API.ts";
const jwt = require('jsonwebtoken');
const AuthContext = createContext<UserContextProps>({} as UserContextProps);

const AuthProvider = ({ children }: Children) => {
  
  const storedUser =localStorage.getItem("user")!
  const [user, setUser] = useState<User| null>(
    JSON.parse(storedUser)
  );
  const [token, setToken] = useState<string | null>(
    localStorage.getItem("polsl-social")
  );
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(
    token === null ? false : true
  );
  const autoLogout = async ()=>{
    if(token){
      const decoded = jwt.decode()
      setTimeout(()=>{logOut()},decoded.exp)
    }
  }
  const refreshUser =async()=>{
    try{
      const res = await fetchUserById(user?.id!)
      if (res.avatarFileName!==null){
        localStorage.setItem("user",JSON.stringify(res))
        location.reload()
      }
      
    }
    catch(e){
      console.log(e)
    }
  }
  const logIn = async (data: Partial<UserDTO>) => {
    //TODO testy

    try {
      const response = await fetch(`${getHost()}User/Login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },

        body: JSON.stringify(data),
      });
      if(response.status===404){
        alert("invalid email or password")
        return;
      }
      const text = await response.text();
      if (text) {
        const res = JSON.parse(text);
        const user1:User = {
          id: res.id,
          avatarFileName: res.avatarFileName,
          username: res.username,
        }
        if (res.token) {
          setUser(user1);
          localStorage.setItem("user",JSON.stringify(user1))
          setToken(res.token);
          localStorage.setItem("polsl-social", res.token);
          setIsAuthenticated(true);
          return;
        }
        throw new Error(res.message);
      }
    } catch (err) {
      alert("invalid email or password")
    }
  };
  const logOut = () => {
    setIsAuthenticated(false);
    setUser(null);
    setToken(null);
    localStorage.removeItem("polsl-social");
    localStorage.removeItem("user");
  };

  return (
    <AuthContext.Provider
      value={{ token, user, isAuthenticated,refreshUser, logIn, logOut,autoLogout }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;

export const useAuth = () => {
  return useContext(AuthContext);
};
