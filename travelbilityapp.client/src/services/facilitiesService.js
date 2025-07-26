import * as api from './api.js';

const endpoint = 'facilities';

export const getFacilities = async () => api.get(endpoint);

export const getAccessibility = async () => api.get(`${endpoint}/accessibility`);

export const getPropertyFacilities = async () => api.get(`${endpoint}/in-property`);

export const getRoomFacilities = async () => api.get(`${endpoint}/in-room`);