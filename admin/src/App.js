import ProtectedRoute from '@components/ProtectedRoute'
import React, { Suspense } from 'react'
import { HashRouter, Route, Routes } from 'react-router-dom'

import 'react-table-6/react-table.css'
import 'simplebar/dist/simplebar.min.css'
import 'sweetalert2/src/sweetalert2.scss'
import "toastify-js/src/toastify.css"
import './scss/style.scss'

const loading = (
  <div className="pt-3 text-center">
    <div className="sk-spinner sk-spinner-pulse"></div>
  </div>
)

// Containers
const DefaultLayout = React.lazy(() => import('@layout/DefaultLayout'))

// Pages
const Login = React.lazy(() => import('@views/pages/login/Login'))
const Register = React.lazy(() => import('@views/pages/register/Register'))
const Page404 = React.lazy(() => import('@views/pages/page404/Page404'))
const Page500 = React.lazy(() => import('@views/pages/page500/Page500'))

const App = () => {
  return (
    <HashRouter>
      <Suspense fallback={loading}>
        <Routes>
          <Route exact path="/login" name="Login Page" element={<Login />} />
          <Route exact path="/404" name="Page 404" element={<Page404 />} />
          <Route exact path="/500" name="Page 500" element={<Page500 />} />
          <Route path="*" name="Home" element={<ProtectedRoute><DefaultLayout /></ProtectedRoute>} />
        </Routes>
      </Suspense>
    </HashRouter>
  )
}

export default App
