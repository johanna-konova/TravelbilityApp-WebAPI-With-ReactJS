import * as api from './api.js';

const endpoint = 'roomTypes';

export const getAll = async () => api.get(endpoint);