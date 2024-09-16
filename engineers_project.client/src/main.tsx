import React from 'react'
import ReactDOM from 'react-dom/client'
import './styles/index.css'
import { RouterProvider } from 'react-router'
import AuthProvider from './Router/AuthProvider.tsx'
import { router } from './Router/Routes.tsx'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
		<AuthProvider>
			<RouterProvider router={router} />
		</AuthProvider>
  </React.StrictMode>,
)
