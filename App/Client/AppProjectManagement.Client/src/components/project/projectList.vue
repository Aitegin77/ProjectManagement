<template>
  <div v-if="loading">
    Loading... Please refresh once the ASP.NET backend has started.
  </div>

  <div v-else>
    <h1>Проекты</h1>
    <router-link :to="`/Project/Create`" class="redirect">Создать новый проект</router-link>
    <table border="1" cellpadding="5">
      <thead>
        <tr>
          <th>№</th>
          <th>Название</th>
          <th>Дата начала</th>
          <th>Дата завершения</th>
          <th>Приоритет</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="p in projects" :key="p.id">
          <td>{{ p.id }}</td>
          <td>
            <router-link :to="`/Project/${p.id}`">{{ p.name }}</router-link>
          </td>
          <td>{{ p.startDate }}</td>
          <td>{{ p.endDate }}</td>
          <td>{{ p.priority }}</td>
          <td><button @click="deleteById(p.id)">Удалить</button></td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue';
  import { getProjects, deleteProject } from '../../api/projectService';

  const projects = ref([]);
  const loading = ref(true);

  onMounted(async () => {
    try {
      projects.value = await getProjects();
    } catch (err) {
      console.error('Ошибка загрузки проектов:', err);
    } finally {
      loading.value = false;
    }
  });

  const deleteById = async (id) => {
    try {
      await deleteProject(id);
    } catch (err) {
      console.error('Не удалось удалить проект:', err)
    } finally {
      projects.value = await getProjects();
    }
  }
</script>
