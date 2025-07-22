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

    const updateData = (elementData) => {
        if (Array.isArray(data)) {
            setData(previousData =>
                previousData.some(pd => pd.id === elementData.id)
                    ? previousData.map(pd => pd.id === elementData.id ? elementData : pd)
                    : [...previousData, elementData]
            );
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