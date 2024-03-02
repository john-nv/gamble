
import React from 'react'

const Login = React.lazy(() => import('@views/Login'))
const Home = React.lazy(() => import('@views/Home'))

export const publicRoutes = [
  { path: '/login', exact: true, name: 'login', element: Login }
]

export const protectedRoutes = [
  { path: '/', exact: true, name: 'home', element: Home }
]