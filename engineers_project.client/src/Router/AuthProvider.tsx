
import { createContext, useContext, useState } from "react";
import { Children } from "../interface/Children";
import { UserContextProps } from "../interface/UserContextProps";
import { User } from "../DTO/User";


const AuthContext = createContext<UserContextProps>({} as UserContextProps);

const AuthProvider = ({ children }: Children) => {
  const [user, setUser] = useState<Partial<User> | null>(null);
  const [token, setToken] = useState<string | null>(
   sessionStorage.getItem("polsl-social")
  );
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(token===null?false:true);

  const logIn = async (data: Partial<User>) => {
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
         // setUser(res.data.user);
          setToken(res.token);
          sessionStorage.setItem("polsl-social", res.token );
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
    console.log("logout success");
    setIsAuthenticated(false);
    setUser(null);
    setToken(null);
    sessionStorage.removeItem("polsl-social");
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
