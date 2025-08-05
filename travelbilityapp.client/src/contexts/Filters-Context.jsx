import { createContext, useContext } from 'react';

export const FiltersContext = createContext({
    propertyTypes: [],
    arePropertyTypesLoaded: false,
    roomTypes: [],
    areRoomTypesLoaded: false,
    facilities: [],
    areFacilitiesLoaded: false,
    filters: {},
    isPagedResultLoaded: false,
    filterHandler: () => {},
});

export const useFiltersContext = () => useContext(FiltersContext);