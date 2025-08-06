import { createContext, useContext, useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import { setErrorHandler } from "../navigation";

export const ErrorContext = createContext();

export default function ErrorProvider({ children }) {
    const navigate = useNavigate();
    const [error, setError] = useState(null);

    useEffect(() => {
        setErrorHandler(setError);
    }, []);

    useEffect(() => {
        if (error) {
            switch (error.status) {
                case 403: navigate("/404"); break;
                case 404: navigate("/404"); break;
                case 500: navigate("/500"); break;
                default: console.error("Unhandled error", error);
            }

            setError(null);
        }
    }, [error, navigate]);

    return (
        <ErrorContext.Provider value={{ setError }}>
            {children}
        </ErrorContext.Provider>
    );
}

export const useErrorHandler = () => useContext(ErrorContext);