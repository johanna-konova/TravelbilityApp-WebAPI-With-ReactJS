import * as yup from "yup";

export const registrationSchema = yup.object().shape({
    email: yup
        .string()
        .email("Please, enter a valid email address.")
        .required("Please, enter your email address."),

    password: yup
        .string()
        .required("Please, enter your password.")
        .min(6, "The password must be at least 6 characters.")
        .max(100, "The password cannot exceed 100 characters."),

    confirmedPassword: yup
        .string()
        .oneOf([yup.ref("password"), null], "The password and confirmation password do not match."),
});

export const loginSchema = yup.object().shape({
    email: yup
        .string()
        .required("Please, enter your email address."),

    password: yup
        .string()
        .required("Please, enter your password."),
});

export const propertySchema = yup.object().shape({
    /*"step-1": yup.object({
        name: yup
            .string()
            .required("Name is required.")
            .min(3, "Name must be at least 3 characters long.")
            .max(100, "Name cannot exceed 100 characters."),

        typeId: yup
            .string()
            .required("Prease, select a type."),

        starsCount: yup
            .string()
            .required('Please, select count of stars.'),

        checkIn: yup
            .string()
            .required('Check-in is required.')
            .matches(/^([01]\d|2[0-3]):([0-5]\d)$/, 'Enter a valid time in HH:mm format.'),

        checkOut: yup
            .string()
            .required('Check-out is required.')
            .matches(/^([01]\d|2[0-3]):([0-5]\d)$/, 'Enter a valid time in HH:mm format.'),

        address: yup
            .string()
            .required('Address is required.')
            .min(10, 'Address must be at least 10 characters long.')
            .max(200, 'Address cannot exceed 200 characters.'),

        description: yup
            .string()
            .required('Description is required.')
            .min(10, 'Description must be at least 10 characters long.')
            .max(1000, 'Description cannot exceed 1000 characters.'),
    }),

    "step-2": yup.object({
        commonFacilityIds: yup
            .array()
            .min(1, "Please, select at least 1 facility."),

        accessibilityIds: yup
            .array()
            .min(1, "Please, select at least 1 accessibility."),
    }),

    "step-3": yup.object({
        imageUrls: yup
            .array()
            .min(5, "Please, upload at least 5 photos."),
    }),*/
});

export const imageUrlSchema = yup
    .string()
    .url('Enter a valid photo URL format.')
    .required('Photo URL cannot be empty.');