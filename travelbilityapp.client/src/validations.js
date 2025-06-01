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