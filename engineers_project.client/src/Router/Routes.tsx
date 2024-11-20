import { createBrowserRouter } from "react-router-dom";
import Dashboard from "../Pages/Dashboard";
import ErrorPage from "../Pages/ErrorPage";
import { LoginPage } from "../Pages/LoginPage";
import PrivateRoute from "./PrivateRoute";
import { FeedPage } from "../Pages/FeedPage";
import { ProfilePage } from "../Pages/ProfilePage";
import { PostPage } from "../Pages/PostPage";

//dorobić więcej jak jedną stronę

export const router = createBrowserRouter([
  {
    path: "/",
    element: <Dashboard />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/",
        element:(
          <PrivateRoute>
            <FeedPage/>
          </PrivateRoute>
        ),
        errorElement: (
          <PrivateRoute>
            <ErrorPage />
          </PrivateRoute>
        ),
      },
    ],
  },
  {
    path: "/post/:id",
    element:(
      <PrivateRoute>
        <PostPage/>
      </PrivateRoute>
    )


  },
  { path:"/profile/:id",
    element:(
      <PrivateRoute>
        <ProfilePage/>
      </PrivateRoute>
    )

  },
  { path:"/group"
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
]);
