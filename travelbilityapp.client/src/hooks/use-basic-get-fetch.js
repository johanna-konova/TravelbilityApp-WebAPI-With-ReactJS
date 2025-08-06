import { useEffect, useState } from "react";

export function useBasicGetFetch(getDataCallbackFunction, initialData = [], dependencies = []) {
    const [data, setData] = useState(initialData);
    const [isDataLoaded, setIsDataLoaded] = useState(false);

    useEffect(() => {
        (async () => {
            setIsDataLoaded(false);

            const data = await getDataCallbackFunction();
            setData(data);

            setIsDataLoaded(true);
        }
        )();
    }, dependencies);

    const updateData = (input) => {
        if (Array.isArray(data)) {
            setData(previousData =>
                typeof input === "function"
                    ? input(previousData) 
                    : previousData.some(pd => pd.id === input.id)
                        ? previousData.map(pd => pd.id === input.id ? input : pd)
                        : [...previousData, input]
            );
        } else if (typeof input === "function") {
            setData(previousData => input(previousData))
        }
    };

    const removeDataElement = (id) => {
        if (Array.isArray(data)) {
            setData(previousData => previousData.filter(pd => pd.id !== id));
        }
    };

    return {
        data,
        isDataLoaded,
        updateData,
        removeDataElement,
    }
}