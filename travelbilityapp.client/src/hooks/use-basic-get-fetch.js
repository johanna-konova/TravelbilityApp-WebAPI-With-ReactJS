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

    const removeDataElement = (id) => {
        if (data.length) {
            setData(previousData => previousData.filter(pd => pd.id !== id));
        }
    };

    return {
        data,
        isDataLoaded,
        removeDataElement,
    }
}