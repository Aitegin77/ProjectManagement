<template>
  <div v-if="loading">
    Loading project details...
  </div>

  <div v-else-if="project">
    <h2>{{ project.name }}</h2>
    <table border="1" cellpadding="5">
      <tbody>
        <tr>
          <th>Заказчик</th>
          <td>{{ project.customer }}</td>
        </tr>
        <tr>
          <th>Исполнитель</th>
          <td>{{ project.performer }}</td>
        </tr>
        <tr>
          <th>Дата начала</th>
          <td>{{ project.startDate }}</td>
        </tr>
        <tr>
          <th>Дата завершения</th>
          <td>{{ project.endDate }}</td>
        </tr>
        <tr>
          <th>Приоритет</th>
          <td>{{ project.priority }}</td>
        </tr>
        <tr>
          <th>Руководитель проекта</th>
          <td>
            <router-link :to="`/Employee/${project.manager?.id}`">
              {{ project.manager?.lastName }} {{ project.manager?.firstName }} {{ project.manager?.patronymic }}
            </router-link>
          </td>
        </tr>
      </tbody>
    </table>
    <h3>Список сотрудников</h3>
    <table border="1" cellpadding="5">
      <thead>
        <tr>
          <th>Id</th>
          <th>ФИО</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="emp in project.employees" :key="emp.id">
          <td>
            <router-link :to="`/Employee/${emp.id}`">{{ emp.id }}</router-link>
          </td>
          <td>
            <router-link :to="`/Employee/${emp.id}`">{{ emp.lastName }} {{ emp.firstName }} {{ emp.patronymic }}</router-link>
          </td>
        </tr>
      </tbody>
    </table>
    <h4>Документы</h4>
    <table border="1" cellpadding="5">
      <thead>
        <tr>
          <th>Название</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="doc in project.documentNames" :key="doc.id">
          <td>{{ doc.name }}</td>
        </tr>
      </tbody>
    </table>
  </div>

  <div v-else>
    <p>Проект не найден.</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { getProjectById } from '../../api/projectService';

const route = useRoute();
const project = ref(null);
const loading = ref(true);

onMounted(async () => {
  const id = route.params.id;
  try {
    project.value = await getProjectById(id);
  } catch (err) {
    console.error('Ошибка загрузки проекта:', err);
  } finally {
    loading.value = false;
  }
});
</script>
