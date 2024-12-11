import { createBrowserRouter, redirect } from "react-router-dom";
import Dashboard from "../Pages/Dashboard";
import ErrorPage from "../Pages/ErrorPage";
import { LoginPage } from "../Pages/LoginPage";
import PrivateRoute from "./PrivateRoute";
import { FeedPage } from "../Pages/FeedPage";
import { ProfilePage } from "../Pages/ProfilePage";
import { PostPage } from "../Pages/PostPage";
import { SearchPage } from "../Pages/SearchPage";
import validator from "validator"
import { GroupPage } from "../Pages/GroupPage";

export const validIdLoader = async ({ params }) => {
  const { id } = params;

  // Validate UUID
  if (!validator.isUUID(id)||id===undefined) {
    return redirect("/");
  }

  return { id }; // Pass valid ID to the component
};

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
            <FeedPage />
          </PrivateRoute>
        ),
        errorElement: (
          <PrivateRoute>
            <ErrorPage />
          </PrivateRoute>
        ),
      },
      {
        path: "/post/:id",
        loader:validIdLoader,
        element: (
          <PrivateRoute>
            <PostPage />
          </PrivateRoute>
        ),
      },
      {
        path: "/profile/:id",
        
        loader:validIdLoader,
        element: (
          <PrivateRoute>
            <ProfilePage />
          </PrivateRoute>
        ),
      },{
        path: "/search/:query",
        element: (
          <PrivateRoute>
            <SearchPage />
          </PrivateRoute>
        ),
      },{ path: "/group",
        loader:validIdLoader,
        element:(
          <PrivateRoute>
            <GroupPage/>
          </PrivateRoute>
        )

       },
    ],
  },

  
  {
    path: "/login",
    element: <LoginPage />,
  },
  { 
    path:"*",
    element:<ErrorPage/>//change
  }
]);

