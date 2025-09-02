import axios from 'axios';

const API_URL = '/api/Project';

export const getProjects = () => axios.get(API_URL).then(res => res.data);
export const getProjectById = id => axios.get(`${API_URL}/${id}`).then(res => res.data);
export const createProject = project => axios.post(`${API_URL}/Create`, project).then(res => res.data);
export const updateProject = project => axios.put(API_URL, project);
export const deleteProject = id => axios.delete(`${API_URL}/${id}`);
