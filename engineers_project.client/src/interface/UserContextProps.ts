import { User } from "../API/DTO/User";
import { UserDTO } from "../API/DTO/UserDTO";

export interface UserContextProps {
	token: string | null;
	user: Partial<User> | null;
	isAuthenticated: boolean | null;
	logIn: (params: Partial<UserDTO>) => Promise<void>;
	refreshUser:()=>void;
	logOut: () => void;
}
