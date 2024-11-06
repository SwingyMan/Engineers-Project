import React from 'react'
import ReactDOM from 'react-dom/client'
import './styles/index.css'
import { RouterProvider } from 'react-router'
import AuthProvider from './Router/AuthProvider.tsx'
import { router } from './Router/Routes.tsx'
import './scss/styles.scss'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
const queryClient= new QueryClient()
ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
	<QueryClientProvider client={queryClient}>

		<AuthProvider>
			<RouterProvider router={router} />
		</AuthProvider>
	</QueryClientProvider>
  </React.StrictMode>,
)
