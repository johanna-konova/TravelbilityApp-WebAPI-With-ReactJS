import * as api from './api.js';

const endpoint = 'bedTypes';

export const getAll = async () => api.get(endpoint);