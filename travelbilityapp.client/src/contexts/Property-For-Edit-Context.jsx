import PropertyContextProvider, { usePropertyContext } from "./Property-Context";

import { getForEditById } from "../services/propertiesService";

export default function PropertyForEditContextProvider({ children }) {
    return (
        <PropertyContextProvider getDataCallbackFunction={getForEditById}>
            {children}
        </PropertyContextProvider>
    );
}

export const usePropertyForEditContext = usePropertyContext;