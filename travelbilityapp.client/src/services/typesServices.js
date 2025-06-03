import * as api from './api.js';

const endpoint = 'propertyTypes';

export const getAll = async () => api.get(endpoint);