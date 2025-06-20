import * as api from './api.js';

const endpoint = 'properties';

export const getAll = async () => api.get(endpoint);

export const getThreeNewestAdded = async () => api.get(`${endpoint}?sortBy=_createdOn%20desc&pageSize=3&load=typeData%3DtypeId%3ApropertyTypes`);

export const getById = async (id) => api.get(`${endpoint}/${id}`);

export const getByOwnerId = async (ownerId) => api.get(`${endpoint}?where=_ownerId%3D%22${ownerId}%22`);

export const create = async (data) => api.post(endpoint, data);

export const edit = async (id, data) => api.put(`${endpoint}/${id}`, data);

export const deleteById = async (id) => api.del(`${endpoint}/${id}`);