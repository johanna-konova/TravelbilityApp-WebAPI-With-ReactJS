import { createContext, useContext } from 'react';

export const FiltersContext = createContext({
    propertyTypes: [],
    arePropertyTypesLoaded: false,
    facilities: [],
    areFacilitiesLoaded: false,
    filters: {},
    isPropertiesDataLoaded: false,
    filterHandler: () => {},
});

export const useFiltersContext = () => useContext(FiltersContext);