
import { createContext, useContext, useState } from "react";
import { Children } from "../interface/Children";
import { UserContextProps } from "../interface/UserContextProps";
import { UserDTO } from "../API/DTO/UserDTO";


const AuthContext = createContext<UserContextProps>({} as UserContextProps);

const AuthProvider = ({ children }: Children) => {
  const [user, setUser] = useState<Partial<User> | null>(null);
  const [token, setToken] = useState<string | null>(
   localStorage.getItem("polsl-social")
  );
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(token===null?false:true);

  const logIn = async (data: Partial<UserDTO>) => {
    //TODO testy

    try {
      const response = await fetch("https://localhost:7290/api/v1/User/Login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        
        body: JSON.stringify(data),
      });

      const text = await response.text();

      if (text) {
        const res = JSON.parse(text);
        console.log(res)
        console.log(res.token)
        if (res.token) {
          setUser({id:res.id,avatarName:res.avatarName,firstName:res.firstName});
          setToken(res.token);
          localStorage.setItem("polsl-social", res.token );
          setIsAuthenticated(true)
          return;
        }
          throw new Error(res.message);
        
      }
    } catch (err) {
      console.error(err);
    }
  };
  const logOut = () => {
    setIsAuthenticated(false);
    setUser(null);
    setToken(null);
    localStorage.removeItem("polsl-social");
  };

  return (
    <AuthContext.Provider
      value={{ token, user, isAuthenticated, logIn, logOut }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;

export const useAuth = () => {
  return useContext(AuthContext);
};
