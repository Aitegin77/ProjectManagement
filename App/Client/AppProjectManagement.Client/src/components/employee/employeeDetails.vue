<template>
  <div v-if="loading">
    Loading employee details...
  </div>

  <div v-else-if="employee">
    <h2>Сотрудник №{{ employee.id }}</h2>
    <table border="1" cellpadding="5">
      <thead>
        <tr>
          <th>Фамилия</th>
          <th>Имя</th>
          <th>Отчество</th>
          <th>Почта</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td>{{ employee.lastName }}</td>
          <td>{{ employee.firstName }}</td>
          <td>{{ employee.patronymic }}</td>
          <td>{{ employee.mail }}</td>
        </tr>
      </tbody>
    </table>
  </div>

  <div v-else>
    <p>Сотрудник не найден.</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { getEmployeeById } from '../../api/employeeService';

const route = useRoute();
const employee = ref(null);
const loading = ref(true);

onMounted(async () => {
  const id = route.params.id;
  try {
    employee.value = await getEmployeeById(id);
    console.log(employee.value);
  } catch (err) {
    console.error('Ошибка загрузки данных сотрудника:', err);
  } finally {
    loading.value = false;
  }
});
</script>
