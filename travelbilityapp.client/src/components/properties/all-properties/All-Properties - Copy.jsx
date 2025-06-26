import  { useEffect, useState } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { Container } from 'react-bootstrap';

import { useAuthContext } from '../../../contexts/Auth-Context';
import { FiltersContext } from '../../../contexts/Filters-Context';
import { PropertiesContext } from '../../../contexts/Properties-Context';

import { useBasicGetFetch } from '../../../hooks/use-basic-get-fetch';
import { getAll } from '../../../services/propertiesService';
import { getAll as getPropertyTypes } from '../../../services/typesServices';
import { getFacilities } from '../../../services/facilitiesService';

import FiltersContainer from '../filters/Filters-Container';
import PropertyShortListView from './Property-Short-List-View';
import { WheelchairTireSpinner } from '../../loaders/Loaders';

import styles from './All-Properties.module.css';

export default function AllProperties() {
    const [filters, setFilters] = useState({
        propertyTypeIds: [],
        facilityIds: [],
        accessibilityIds: [],
    });

    const [searchParams] = useSearchParams();
    const navigate = useNavigate();

    const { id } = useAuthContext();

    const searchParamsEntries = Object.fromEntries(searchParams);

    const {
        data: propertiesData,
        isDataLoaded: isPropertiesDataLoaded,
        removeDataElement: deletePropertyByIdHandler } = useBasicGetFetch(() => getAll(filters), [], [filters]);

    const {
        data: propertyTypes,
        isDataLoaded: arePropertyTypesLoaded } = useBasicGetFetch(() => getPropertyTypes());

    const {
        data: facilities,
        isDataLoaded: areFacilitiesLoaded } = useBasicGetFetch(() => getFacilities());

    useEffect(() => {
        console.log('filters');
        setFilters(previousFilters => Object.keys(previousFilters)
            .reduce((updatedFilters, k) => {
                updatedFilters[k] = searchParamsEntries[k]?.split(",") || [];
                return updatedFilters;
            }, { ...previousFilters })
        );
    }, [searchParams]);

    useEffect(() => {
        console.log('searchParams');
        const searchParams = Object.entries(filters)
            .filter(([k, v]) => v.length > 0)
            .map(([k, v]) => `${k}=${v.join(",")}`)
            .join("&");

        navigate(`/properties?${searchParams}`, { replace: true });
    }, [filters]);

    const setFiltersHandler = (name, id) => setFilters(previousFilters => ({
        ...previousFilters,
        [name]: previousFilters[name].includes(id)
            ? previousFilters[name].filter(pfId => pfId !== id)
            : [...previousFilters[name], id]
    }));

    return (
        <Container className="mt-5 d-flex">
            <FiltersContext.Provider value={{
                propertyTypes,
                arePropertyTypesLoaded,
                facilities,
                areFacilitiesLoaded,
                filters,
                isPropertiesDataLoaded,
                filterHandler: setFiltersHandler }}
            >
                <FiltersContainer />
            </FiltersContext.Provider>

            <div className={styles["properties-container"]}>
                <div className={styles["found"]}>
                    <span>Found suitable properties: {propertiesData.length}</span>
                </div>
                {isPropertiesDataLoaded
                    ? <PropertiesContext.Provider value={{ deletePropertyByIdHandler }}>
                        {propertiesData.map(pd =>
                            <PropertyShortListView
                                key={pd.id}
                                {...pd}
                                isLoggedInUserPropertyDataCreator={id === pd._ownerId}
                            />
                        )}
                    </PropertiesContext.Provider>
                    : <WheelchairTireSpinner style={{ minHeight: "calc(100vh - 270px)" }} />
                }
            </div>

        </Container>
    )
};
