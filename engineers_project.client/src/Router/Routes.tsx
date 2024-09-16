import { createBrowserRouter } from "react-router-dom";
import Dashboard from "../Pages/Dashboard";
import ErrorPage from "../Pages/ErrorPage";
import { LoginPage } from "../Pages/LoginPage";
import PrivateRoute from "./PrivateRoute";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <Dashboard />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/",
        element: (
          <PrivateRoute>
            <ErrorPage />
          </PrivateRoute>
        ),
      },
    ],
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
]);
