import * as api from './api.js';

const endpoint = 'rooms';

export const getAll = async (propertyId) => api.get(`${endpoint}?propertyId=${propertyId}`);

export const getAllDetailed = async (propertyId) => api.get(`${endpoint}/detailed?propertyId=${propertyId}`);

export const getById = async (roomId, propertyId) => api.get(`${endpoint}/${roomId}?propertyId=${propertyId}`);

export const getForEditById = async (roomId, propertyId) => api.get(`${endpoint}/${roomId}/for-edit?propertyId=${propertyId}`);

export const create = async (data) => api.post(endpoint, data);

export const edit = async (roomId, data) => api.put(`${endpoint}/${roomId}`, data);

export const deleteById = async (roomId, propertyId) => api.del(`${endpoint}/${roomId}?propertyId=${propertyId}`);