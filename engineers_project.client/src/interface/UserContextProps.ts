import { User } from "../DTO/User";

export interface UserContextProps {
	token: string | null;
	user: Partial<User> | null;
	isAuthenticated: boolean | null;
	logIn: (params: Partial<User>) => Promise<void>;
	logOut: () => void;
}
