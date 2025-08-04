import { Navigate } from "react-router-dom";
import { useAuthContext } from "../../contexts/Auth-Context";

export default function AdminGuard({ children }) {
    const { isAdmin } = useAuthContext();

    return isAdmin
        ? children
        : <Navigate to="/" replace />;
}
