import * as yup from "yup";

export const registrationSchema = yup.object().shape({
    email: yup
        .string()
        .email("Please, enter a valid Email address.")
        .required("Please, enter your Email address."),

    password: yup
        .string()
        .required("Please, enter your Password.")
        .min(6, "The Password must be at least 6 characters long.")
        .max(100, "The Password must be at most 100 characters long."),

    confirmedPassword: yup
        .string()
        .oneOf([yup.ref("password"), null], "The Password and Confirmation Password do not match."),
});

export const loginSchema = yup.object().shape({
    email: yup
        .string()
        .required("Please, enter your Email address."),

    password: yup
        .string()
        .required("Please, enter your Password."),
});

export const propertySchema = yup.object().shape({
    "step-1": yup.object({
        name: yup
            .string()
            .required("Please, enter a Name.")
            .min(3, "The Name must be at least 3 characters long.")
            .max(100, "The Name must be at most 100 characters long."),

        typeId: yup
            .string()
            .required("Prease, select a Type."),

        checkIn: yup
            .string()
            .required('Please, enter a valid time in HH:mm format.')
            .matches(/^([01]\d|2[0-3]):([0-5]\d)$/, 'Please, enter a valid time in HH:mm format.'),

        checkOut: yup
            .string()
            .required('Please, enter a valid time in HH:mm format.')
            .matches(/^([01]\d|2[0-3]):([0-5]\d)$/, 'Please, enter a valid time in HH:mm format.'),

        address: yup
            .string()
            .required('Please, enter an Address.')
            .min(10, 'The Address must be at least 10 characters long.')
            .max(200, 'The Address must be at most 200 characters long.'),

        description: yup
            .string()
            .required('Please, enter a Description.')
            .min(20, 'The Description must be at least 20 characters long.')
            .max(1000, 'The Description must be at most 1000 characters long.'),
    }),

    "step-2": yup.object({
        commonFacilityIds: yup
            .array()
            .min(1, "Please, select at least 1 Facility."),
        
        accessibilityIds: yup
            .array()
            .min(1, "Please, select at least 1 Accessibility."),
    }),

    "step-3": yup.object({
        photoUrls: yup
            .array()
            .min(5, "Please, upload at least 5 Photos with a valid URL format."),
    }),
});

export const roomSchema = yup.object().shape({
    "step-1": yup.object({
        roomTypeId: yup
            .string()
            .typeError("Please, select a Type.")
            .required("Please, select a Type."),

        maxGuests: yup
            .number()
            .typeError("Please, select Maximum count of guests.")
            .required("Please, select Maximum count of guests."),

        pricePerNight: yup
            .number()
            .typeError("Please, enter a valid Price per night.")
            .required("Please, enter a valid Price per night.")
            .min(0.01, "The Price per night must be at least 0.01 EUR."),

        mainBedTypeId: yup
            .number()
            .typeError("Please, select a Main bed type.")
            .required("Please, select a Main bed type."),

        size: yup
            .number()
            .typeError("Please, enter a valid Room size.")
            .required("Please, enter a valid Room size.")
            .min(5, "The Room size must be at least 5 m².")
            .max(5000, "The Room size must be at most 5000 m²."),

        numberOfUnits: yup
            .number()
            .typeError("Please, enter a valid Number of Rooms.")
            .required("Please, enter a valid Number of Rooms.")
            .min(1, "The Number of Rooms must be at least 1.")
            .max(8000, "The Number of Rooms must be at most 8000."),

        description: yup
            .string()
            .required("Please, enter a Description.")
            .min(10, "The Description must be at least 10 characters long.")
            .max(1000, "The Description must be at most 1000 characters long."),
    }),

    "step-2": yup.object({
        commonFacilityIds: yup
            .array()
            .min(1, "Please, select at least 1 Facility."),
    }),

    "step-3": yup.object({
        photoUrls: yup
            .array()
            .min(5, "Please, upload at least 5 Photos with a valid URL format."),
    }),
});

export const imageUrlSchema = yup
    .string()
    .url('Please, enter a valid Photo URL format.')
    .required('Please, enter a valid Photo URL format.');