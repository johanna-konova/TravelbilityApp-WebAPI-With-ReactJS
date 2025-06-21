import { createContext, useContext } from 'react';

export const PropertiesContext = createContext({ deletePropertyByIdHandler: undefined });

export const usePropertiesContext = () => useContext(PropertiesContext);