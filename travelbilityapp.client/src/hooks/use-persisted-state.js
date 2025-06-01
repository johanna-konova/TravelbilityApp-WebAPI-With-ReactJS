import { useState } from "react";

export default function usePersistedState(key, initialState) {
    const [state, setState] = useState(() => {
        const persistedAuthData = localStorage.getItem(key);

        return persistedAuthData === null
            ? typeof initialState === 'function'
                ? initialState()
                : initialState
            : JSON.parse(persistedAuthData);
    });

    const updateState = (authData) => {
        setState(authData);

        authData === null
            ? localStorage.removeItem(key)
            : localStorage.setItem(key, JSON.stringify(authData));
    };

    return [state, updateState]
}