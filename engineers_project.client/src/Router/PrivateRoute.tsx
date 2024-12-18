import { Navigate, Outlet } from "react-router";
import { Children } from "../interface/Children";
import { useAuth } from "./AuthProvider";

const PrivateRoute = ({ children }: Children) => {
	const { isAuthenticated } = useAuth();
	if (!isAuthenticated) return <Navigate to="/login" />;
	return children ? <>{children}</> : <Outlet />;
};

export default PrivateRoute;
