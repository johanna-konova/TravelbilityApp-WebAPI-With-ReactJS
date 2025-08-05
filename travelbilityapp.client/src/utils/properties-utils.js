export const getFilterIds = (searchParams, key) =>
    searchParams.get(key)?.split(",").filter(v => /^\d+$/.test(v)).map(Number) ?? [];

export const constructURLSearchParams = (currentPageNumber, filters) => {
    const params = new URLSearchParams();
    params.append("currentPageNumber", currentPageNumber);

    Object.entries(filters)
        .forEach(([name, ids]) => ids.forEach(id => params.append(name, id)));

    return params.toString();
};

export const areParamsEqual = (firstParam, secondParam) => {
    const toSortedString = param =>
        Array.from(param.entries())
            .sort(([k1, v1], [k2, v2]) =>
                k1.localeCompare(k2) || v1.localeCompare(v2)
            )
            .map(([k, v]) => `${k}=${v}`)
            .join("&");

    return toSortedString(firstParam) === toSortedString(secondParam);
};