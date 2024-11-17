import { UserDTO } from "../DTO/UserDTO";

export interface UserContextProps {
	token: string | null;
	user: Partial<UserDTO> | null;
	isAuthenticated: boolean | null;
	logIn: (params: Partial<UserDTO>) => Promise<void>;
	logOut: () => void;
}
