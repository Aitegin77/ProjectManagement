<template>
  <div v-if="loading">
    Loading... Please refresh once the ASP.NET backend has started.
  </div>

  <div v-else>
    <h1>Сотрудники</h1>
    <router-link :to="`/Employee/Create`">Добавить нового сотрудника</router-link>
    <table border="1" cellpadding="5">
      <thead>
        <tr>
          <th>№</th>
          <th>ФИО</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="e in employees" :key="e.id">
          <td>
            <router-link :to="`/Employee/${e.id}`">{{ e.id }}</router-link>
          </td>
          <td>
            <router-link :to="`/Employee/${e.id}`">{{ e.lastName }} {{ e.firstName }} {{ e.patronymic }}</router-link>
          </td>
          <td><button @click="deleteById(e.id)">Удалить</button></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue';
  import { getEmployees, deleteEmployee } from '../../api/employeeService';

  const employees = ref([]);
  const loading = ref(true);

  onMounted(async () => {
    try {
      employees.value = await getEmployees();
    } catch (err) {
      console.error('Ошибка загрузки сотрудников:', err);
    } finally {
      loading.value = false;
    }
  });

  const deleteById = async (id) => {
    loading.value = true;
    try {
      await deleteEmployee(id);
    } catch (err) {
      if (err.response && err.response.status === 500) {
        alert('Перед удалением сотрудника необходимо снять его с должности руководителя проекта!');
      } else {
        console.error('Не удалось удалить сотрудника:', err);
        alert('Произошла ошибка при удалении сотрудника');
      }
    } finally {
      employees.value = await getEmployees();
      loading.value = false;
    }
  };
</script>
