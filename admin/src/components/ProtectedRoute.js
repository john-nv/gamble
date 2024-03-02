import auth from "@services/auth";
import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ children }) => {
  if (!auth.isLoggined()) {
    // user is not authenticated
    return <Navigate to="/login" />;
  }

  return children;
}

export default ProtectedRoute