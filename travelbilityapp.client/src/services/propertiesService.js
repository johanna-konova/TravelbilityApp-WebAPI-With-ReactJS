import * as api from './api.js';

const endpoint = 'properties';

export const getAll = async (params) => api.get(`${endpoint}?${params.toString()}`);

export const getNewestAdded = async (count) => api.get(`${endpoint}/newest?count=${count}`);

export const getById = async (id) => api.get(`${endpoint}/${id}`);

export const getForEditById = async (id) => api.get(`${endpoint}/${id}/for-edit`);

export const getAllForAdmin = async (currenPageNumber) => api.get(`${endpoint}/admin?currenPageNumber=${currenPageNumber}`);

export const getAllByPublisherId = async (currenPageNumber) => api.get(`${endpoint}/listed?currenPageNumber=${currenPageNumber}`);

export const getByPublisherId = async (id) => api.get(`${endpoint}/listed/${id}`);

export const create = async (data) => api.post(endpoint, data);

export const edit = async (id, data) => api.put(`${endpoint}/${id}`, data);

export const deleteById = async (id) => api.del(`${endpoint}/${id}`);

export const sendForApproval = async (id) => api.put(`${endpoint}/${id}/send-for-approval`);

export const publish = async (id) => api.put(`${endpoint}/${id}/publish`);

export const reject = async (id) => api.put(`${endpoint}/${id}/reject`);