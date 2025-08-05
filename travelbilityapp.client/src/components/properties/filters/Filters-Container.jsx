import { useFiltersContext } from "../../../contexts/Filters-Context";

import Filter from "./Filter";

import styles from './../all-properties/All-Properties.module.css';

export default function FiltersContainer() {
    const {
        propertyTypes,
        arePropertyTypesLoaded,
        roomTypes,
        areRoomTypesLoaded,
        facilities,
        areFacilitiesLoaded,
        filters
    } = useFiltersContext();

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
                filterLabel="Room types"
                filterName="roomTypeIds"
                filter={roomTypes}
                isFilterLoaded={areRoomTypesLoaded}
                selectedFilterIds={filters["roomTypeIds"]}
            />

            <Filter
                filterLabel="Property Facilities"
                filterName="propertyFacilityIds"
                filter={facilities.filter(f => f.isForAccessibility === false && (f.whereStatus === "OnlyInCommonArea" || f.whereStatus === "Both"))}
                isFilterLoaded={areFacilitiesLoaded}
                selectedFilterIds={filters["propertyFacilityIds"]}
            />

            <Filter
                filterLabel="Property Accessibility"
                filterName="propertyAccessibilityIds"
                filter={facilities.filter(f => f.isForAccessibility && (f.whereStatus === "OnlyInCommonArea" || f.whereStatus === "Both"))}
                isFilterLoaded={areFacilitiesLoaded}
                selectedFilterIds={filters["propertyAccessibilityIds"]}
            />

            <Filter
                filterLabel="Room Facilities"
                filterName="roomFacilityIds"
                filter={facilities.filter(f => f.isForAccessibility === false && (f.whereStatus === "OnlyInRoom" || f.whereStatus === "Both"))}
                isFilterLoaded={areFacilitiesLoaded}
                selectedFilterIds={filters["roomFacilityIds"]}
            />

            <Filter
                filterLabel="Room Accessibility"
                filterName="roomAccessibilityIds"
                filter={facilities.filter(f => f.isForAccessibility && (f.whereStatus === "OnlyInRoom" || f.whereStatus === "Both"))}
                isFilterLoaded={areFacilitiesLoaded}
                selectedFilterIds={filters["roomAccessibilityIds"]}
            />
        </div>
    )
};
