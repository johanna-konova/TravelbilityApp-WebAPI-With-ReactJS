import * as api from './api.js';

const endpoint = 'properties';

export const getAll = async (params) => api.get(`${endpoint}?${params.toString()}`);

export const getNewestAdded = async (count) => api.get(`${endpoint}/newest?count=${count}`);

export const getById = async (id) => api.get(`${endpoint}/${id}`);

export const getByPublisherId = async () => api.get(`${endpoint}/listed`);

export const create = async (data) => api.post(endpoint, data);

export const edit = async (id, data) => api.put(`${endpoint}/${id}`, data);

export const deleteById = async (id) => api.del(`${endpoint}/${id}`);