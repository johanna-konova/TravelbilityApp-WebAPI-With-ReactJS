import { useFiltersContext } from "../../../contexts/Filters-Context";

import Filter from "./Filter";

import styles from './../all-properties/All-Properties.module.css';

export default function FiltersContainer() {
    const { propertyTypes, arePropertyTypesLoaded, facilities, areFacilitiesLoaded, filters } = useFiltersContext();
    //console.log(filters["propertyTypeIds"]);

    return (
        <div className={styles["filters-container"]}>

            <div className={styles["filter-by"]}>Filter by:</div>

            <Filter
                filterLabel="Property types"
                filterName="propertyTypeIds"
                filter={propertyTypes}
                isFilterLoaded={arePropertyTypesLoaded}
                selectedFilterIds={filters["propertyTypeIds"]}
            />

            <Filter
                filterLabel="Facilities"
                filterName="facilityIds"
                filter={facilities.filter(f => f.isForAccessibility === false)}
                isFilterLoaded={areFacilitiesLoaded}
                selectedFilterIds={filters["facilityIds"]}
            />

            <Filter
                filterLabel="Accessibility"
                filterName="accessibilityIds"
                filter={facilities.filter(f => f.isForAccessibility)}
                isFilterLoaded={areFacilitiesLoaded}
                selectedFilterIds={filters["accessibilityIds"]}
            />
        </div>
    )
};
