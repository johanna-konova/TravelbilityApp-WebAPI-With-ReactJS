export const generateStarIcons = (starsCount) => new Array(starsCount)
    .fill(null)
    .map((_, index) => (<i key={index} className="fas fa-star"></i>));

export const getCountryName = (address) =>
    address.substring(address.lastIndexOf(",") + 1).trim();

export const constructPropertyDataForEditing = (propertyData) => {
    return {
        "id": propertyData.id,
        "step-1": {
            name: propertyData.name,
            typeId: propertyData.type.id,
            starsCount: propertyData.starsCount,
            checkIn: propertyData.checkIn,
            checkOut: propertyData.checkOut,
            address: propertyData.address,
            description: propertyData.description,
        },
        "step-2": {
            commonFacilityIds: propertyData.facilities
                .filter(f => f.isForAccessibility === false)
                .map(f => String(f.id)),
            accessibilityIds: propertyData.facilities
                .filter(f => f.isForAccessibility)
                .map(f => String(f.id)),
        },
        "step-3": {
            photoUrls: propertyData.photoUrls?.map((iu, i) => ({ id: i + 1, url: iu })),
        }
    };
};

export const formatCreatePropertyErrorsData = (formEntries, rawErrorsData) => {
    const errorsData = Object.entries(formEntries)
        .reduce((acc, entry) => {
            if (entry[0].startsWith("step")) {
                acc[entry[0]] = Object.keys(entry[1])
                    .reduce((insideAcc, k) => {
                        const key = `${k.charAt(0).toUpperCase()}${k.slice(1)}`;
                        rawErrorsData[key] && (insideAcc[key] = rawErrorsData[key]);

                        return insideAcc;
                    }, {});
            }
            return acc;
        }, {});

    errorsData["step-3"].ImageUrls = Object.entries(rawErrorsData)
        .filter(([k, v]) => k.startsWith("ImageUrls["))
        .reduce((acc, [k, v]) => {
            const match = k.match(/\[(\d+)\]/);
            if (match) {
                acc[match[1]] = v;
            }
            return acc;
        }, {});

    if (rawErrorsData.ImageUrlsCount !== undefined) {
        errorsData["step-3"].ImageUrlsCount = rawErrorsData.ImageUrlsCount;
    }

    return errorsData;
};