import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
//import './index.css'
import App from './App.jsx'
import CreateOrder from './components/CreateOrder.jsx'
import CurrentOrder from './components/CurrenrOrder.jsx'
import AllOrders from './components/AllOrders.jsx'
import {createBrowserRouter, RouterProvider} from "react-router"

const router = createBrowserRouter([
    { path: '/', Component: App, children: [
      { path: '', Component: AllOrders },
      { path: '/createOrder', Component: CreateOrder },
      { path: '/order/:id', Component: CurrentOrder }
    ]}
]);

createRoot(document.getElementById('root')).render(
  <RouterProvider router={router}/>
)
