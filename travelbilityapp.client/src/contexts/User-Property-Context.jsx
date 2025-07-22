import PropertyContextProvider, { usePropertyContext } from "./Property-Context";

import { getByPublisherId } from "../services/propertiesService";

export default function UserPropertyContextProvider({ children }) {
    return (
        <PropertyContextProvider getDataCallbackFunction={getByPublisherId}>
            {children}
        </PropertyContextProvider>
    );
}

export const useUserPropertyContext = usePropertyContext;