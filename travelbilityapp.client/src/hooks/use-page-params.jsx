import { useCallback } from 'react';
import { useSearchParams } from 'react-router-dom';

export function usePageParams() {
    const [searchParams, setSearchParams] = useSearchParams();

    const currentPage = parseInt(searchParams.get("page") || "1", 10);

    const updatePage = useCallback((nextPage) => {
        const nextParams = new URLSearchParams(searchParams);
        nextParams.set("page", nextPage.toString());
        setSearchParams(nextParams, { replace: true });
    }, [searchParams, setSearchParams]);

    return [currentPage, updatePage];
}
