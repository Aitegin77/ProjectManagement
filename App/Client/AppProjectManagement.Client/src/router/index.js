import { createRouter, createWebHistory } from 'vue-router';
import projectList from '../components/project/projectList.vue';
import projectDetails from '../components/project/projectDetails.vue';
import projectForm from '../components/project/projectForm.vue';

import employeeList from '../components/employee/employeeList.vue';
import employeeDetails from '../components/employee/employeeDetails.vue';
import employeeForm from '../components/employee/employeeForm.vue';


const routes = [
  { path: '/Project', component: projectList },
  { path: '/Project/:id', component: projectDetails, props: true },
  { path: '/Project/Create', component: projectForm },

  { path: '/Employee', component: employeeList },
  { path: '/Employee/:id', component: employeeDetails, props: true },
  { path: '/Employee/Create', component: employeeForm },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
