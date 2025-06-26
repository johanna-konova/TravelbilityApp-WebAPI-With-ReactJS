import * as api from './api.js';

const endpoint = 'facilities';

export const getFacilities = async () => api.get(endpoint);

export const getAccessibility = async () => api.get(`${endpoint}/accessibility`);