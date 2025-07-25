import { useEffect } from "react";
import { Navigate, Outlet } from "react-router-dom";
import toast from "react-hot-toast";

import { useAuthContext } from "../../contexts/Auth-Context";

export default function AuthGuard() {
    const { isAuthenticated } = useAuthContext();

    useEffect(() => {
        if (!isAuthenticated) {
            toast.error("You must be logged in to access that page.");
        }
    }, [isAuthenticated]);

    if (!isAuthenticated) {
        return <Navigate to="/login" replace />;
    }

    return <Outlet />;
}
