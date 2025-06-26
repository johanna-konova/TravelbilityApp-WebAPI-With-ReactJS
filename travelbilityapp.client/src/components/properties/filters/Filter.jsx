import { useFiltersContext } from '../../../contexts/Filters-Context';

import { AccessibilityFilterLoader, FacilitiesFilterLoader, PropertyTypeFilterLoader } from "../../loaders/Loaders";

import styles from './../all-properties/All-Properties.module.css';

const filterLoaders = {
    "Property types": <PropertyTypeFilterLoader />,
    "Facilities": <FacilitiesFilterLoader />,
    "Accessibility": <AccessibilityFilterLoader />,
};

export default function Filter({
    filterLabel,
    filterName,
    isFilterLoaded,
    filter,
    selectedFilterIds,
}) {
    const { isPropertiesDataLoaded, filterHandler } = useFiltersContext();

    return (
        <div className={styles["filter-boxes"]}>
            <label>{filterLabel}</label>
            {isFilterLoaded
                ? filter.map(f =>
                    <div key={f.id}>
                        <label className={styles["custom-checkbox"]}>
                            <input
                                type="checkbox"
                                value={f.id}
                                checked={selectedFilterIds.includes(f.id)}
                                disabled={!isPropertiesDataLoaded}
                                onChange={() => filterHandler(filterName, f.id)}
                            />
                            <span className={styles["checkmark"]}></span>
                            <span style={{ color: isPropertiesDataLoaded ? "#656565" : "lightgray" }}>{f.name}</span>
                        </label>
                    </div>
                  )
                : filterLoaders[filterLabel]
            }
        </div>
    )
};
