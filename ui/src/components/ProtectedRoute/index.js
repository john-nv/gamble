import DefaultLayout from "@components/DefaultLayout";
import auth from "@services/auth";
import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ children }) => {
  if (!auth.isLoggined()) {
    return <Navigate to="/dang-nhap" />;
  }

  return <DefaultLayout>{children}</DefaultLayout>
}

export default ProtectedRoute