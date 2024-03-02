import auth from "@services/auth"
import { Navigate } from "react-router-dom"

const AuthenLayout = (props) => {
  if (auth.isLoggined())
    return <Navigate to="/" />

  return (
    <div className="login-layout d-flex flex-column flex-fill">
      <div className="login-bg login-bg-top flex-fill h-25"></div>
      <div className="login-bg login-bg-middle flex-fill d-flex flex-column justify-content-center align-items-center h-50">
        {props.children}
      </div>
      <div className="login-bg login-bg-bottom flex-fill h-25"></div>
    </div>
  )
}

export default AuthenLayout