import axios from 'axios';

const API_URL = '/api/Employee';

export const getEmployees = (filter) => axios.get(API_URL, { params: filter ? { filter } : {} }).then(res => res.data);
export const getEmployeeById = id => axios.get(`${API_URL}/${id}`).then(res => res.data);
export const createEmployee = employee => axios.post(`${API_URL}/Create`, employee).then(res => res.data);
export const updateEmployee = employee => axios.put(API_URL, employee);
export const deleteEmployee = id => axios.delete(`${API_URL}/${id}`);
