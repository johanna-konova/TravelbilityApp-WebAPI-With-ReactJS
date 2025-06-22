import { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";
import toast from "react-hot-toast";

import { useAuthContext } from "../../contexts/Auth-Context";
import { usePropertyContext } from "../../contexts/Property-Context";

export default function PublisherGuard({ children }) {
    const { id } = useAuthContext();
    const { propertyData } = usePropertyContext();

    const [hasRedirect, setHasRedirect] = useState(false);

    useEffect(() => {
        if (propertyData.id && propertyData.publisherId !== id) {
            toast.error("You can only edit the properties you have published.");
            setHasRedirect(true);
        }
    }, [id, propertyData]);

    if (hasRedirect) {
        return <Navigate to="/my-properties" replace />;
    }

    return propertyData.id !== undefined ? children : null;
}
