import  { useMemo, useCallback } from 'react';
import { useSearchParams } from 'react-router-dom';
import { Container } from 'react-bootstrap';

import { useAuthContext } from '../../../contexts/Auth-Context';
import { FiltersContext } from '../../../contexts/Filters-Context';
import { PropertiesContext } from '../../../contexts/Properties-Context';

import { useBasicGetFetch } from '../../../hooks/use-basic-get-fetch';
import { getAll } from '../../../services/propertiesService';
import { getAll as getPropertyTypes } from '../../../services/typesServices';
import { getAll as getRoomTypes } from '../../../services/roomTypesService';
import { getFacilities } from '../../../services/facilitiesService';
import { areParamsEqual, constructURLSearchParams, getFilterIds } from '../../../utils/properties-utils';

import FiltersContainer from '../filters/Filters-Container';
import PropertyShortListView from './Property-Short-List-View';
import { WheelchairTireSpinner } from '../../loaders/Loaders';

import styles from './All-Properties.module.css';

export default function AllProperties() {
    const { id } = useAuthContext();
    const [searchParams, setSearchParams] = useSearchParams();

    const filters = useMemo(() => ({
        propertyTypeIds: getFilterIds(searchParams, "propertyTypeIds"),
        roomTypeIds: getFilterIds(searchParams, "roomTypeIds"),
        propertyFacilityIds: getFilterIds(searchParams, "propertyFacilityIds"),
        roomFacilityIds: getFilterIds(searchParams, "roomFacilityIds"),
        propertyAccessibilityIds: getFilterIds(searchParams, "propertyAccessibilityIds"),
        roomAccessibilityIds: getFilterIds(searchParams, "roomAccessibilityIds"),
    }), [searchParams]);

    const {
        data: propertiesData,
        isDataLoaded: isPropertiesDataLoaded,
        removeDataElement: deletePropertyByIdHandler,
    } = useBasicGetFetch(() => getAll(constructURLSearchParams(filters)), [], [filters]);

    const {
        data: propertyTypes,
        isDataLoaded: arePropertyTypesLoaded,
    } = useBasicGetFetch(() => getPropertyTypes());

    const {
        data: roomTypes,
        isDataLoaded: areRoomTypesLoaded,
    } = useBasicGetFetch(() => getRoomTypes());

    const {
        data: facilities,
        isDataLoaded: areFacilitiesLoaded,
    } = useBasicGetFetch(() => getFacilities());

    const toggleFilter = useCallback((name, id) => {
        const current = new Set(filters[name]);

        current.has(id)
            ? current.delete(id)
            : current.add(id);

        const nextParams = new URLSearchParams(searchParams);

        current.size
            ? nextParams.set(name, Array.from(current).join(","))
            : nextParams.delete(name);

        if (areParamsEqual(nextParams, searchParams) === false) {
            setSearchParams(nextParams, { replace: true });
        }
    }, [filters, searchParams, setSearchParams]);

    return (
        <Container className="mt-5 d-flex">
            <FiltersContext.Provider value={{
                propertyTypes,
                arePropertyTypesLoaded,
                roomTypes,
                areRoomTypesLoaded,
                facilities,
                areFacilitiesLoaded,
                filters,
                isPropertiesDataLoaded,
                filterHandler: toggleFilter
            }}
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
                                isLoggedInUserPropertyDataCreator={id === pd.publisherId}
                            />
                        )}
                    </PropertiesContext.Provider>
                    : <WheelchairTireSpinner style={{ minHeight: "calc(100vh - 270px)" }} />
                }
            </div>

        </Container>
    )
};