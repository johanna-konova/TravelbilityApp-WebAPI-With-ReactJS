import { useAuthContext } from "../contexts/Auth-Context";

import * as api from '../services/api';

const endpoints = {
    REGISTER: 'account/register',
    LOGIN: 'account/login',
    LOGOUT: 'account/logout'
};

export function useLogin() {
    const { changeLoggedInUserData } = useAuthContext();

    const loginHandler = async (userData) => {
        const response = await api.post(endpoints.LOGIN, userData);
        changeLoggedInUserData(response);
    }
    
    return { loginHandler };
}

export function useRegister() {
    const { changeLoggedInUserData } = useAuthContext();

    const registerHandler = async (userData) => {
        const response = await api.post(endpoints.REGISTER, userData);
        changeLoggedInUserData(response);
    }
    
    return { registerHandler };
}

export function useLogout() {
    const { refreshToken, changeLoggedInUserData } = useAuthContext();

    const logoutHandler = async () => {
        try {
            await api.post(endpoints.LOGOUT, { refreshToken });
            changeLoggedInUserData({});
        } catch (error) {
            if (error.code === 403) {
                changeLoggedInUserData({});
            }
        }
    }
    
    return { logoutHandler };
}