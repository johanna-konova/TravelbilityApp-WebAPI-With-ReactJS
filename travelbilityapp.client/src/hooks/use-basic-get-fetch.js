import { useEffect, useState } from "react";

import { useAuthContext } from "../contexts/Auth-Context";

export function useBasicGetFetch(getDataCallbackFunction, initialData = [], dependencies = []) {
    const [data, setData] = useState(initialData);
    const [isDataLoaded, setIsDataLoaded] = useState(false);

    const { changeLoggedInUserData } = useAuthContext();

    useEffect(() => {
        (async () => {
            setIsDataLoaded(false);
            
            try {
                const data = await getDataCallbackFunction();
    
                setData(data);
            } catch (error) {
                if (error.code === 403) {
                    changeLoggedInUserData({});
                }
            } finally {
                setIsDataLoaded(true);
            }
        }
        )();
    }, dependencies);

    const removeDataElement = (id) => {
        if (data.length) {
            setData(previousData => previousData.filter(pd => pd._id !== id));
        }
    };

    return {
        data,
        isDataLoaded,
        removeDataElement,
    }
}