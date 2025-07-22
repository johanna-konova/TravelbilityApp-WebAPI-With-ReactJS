import { createContext, useContext, useEffect, useState } from "react";
import { useLocation, useNavigate, useParams } from "react-router-dom";

import { getById } from "../services/propertiesService";

export const PropertyContext = createContext({
    propertyId: '',
    propertyData: {},
    propertyCreatorId: '',
    isPropertyDataLoaded: false,
});

export default function PropertyContextProvider({
    children,
    getDataCallbackFunction = getById
}) {
    const { propertyId } = useParams();

    const [propertyData, setPropertyData] = useState({});
    const [isPropertyDataLoaded, setIsPropertyDataLoaded] = useState(false);

    const navigate = useNavigate();
    const location = useLocation();

    useEffect(() => {
        if (propertyId !== undefined) {
            (async () => {
                try {
                    const propertyData = await getDataCallbackFunction(propertyId);
                    
                    setPropertyData(propertyData);
                    setIsPropertyDataLoaded(true);
                } catch (error) {
                    if (error.status === 400 ||
                        error.status === 404) {
                        navigate("/404");
                    }
                }
            }
            )()
        }
    }, [propertyId, location.pathname]);

    const contextData = {
        propertyId,
        propertyData,
        propertyCreatorId: propertyData.publisherId,
        isPropertyDataLoaded,
    };

    return (
        <>
            <PropertyContext.Provider value={contextData}>
                {children}
            </PropertyContext.Provider>
        </>
    )
}

export const usePropertyContext = () => useContext(PropertyContext);