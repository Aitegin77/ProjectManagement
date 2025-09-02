<template>
  <form @submit.prevent="submit">
    <table>
      <tbody>
        <tr>
          <td><label>Название проекта</label></td>
          <td><input type="text" v-model="project.name" /></td>
        </tr>
        <tr>
          <td><label>Заказчик</label></td>
          <td><input type="text" v-model="project.customer" /></td>
        </tr>
        <tr>
          <td><label>Исполнитель</label></td>
          <td><input type="text" v-model="project.performer" /></td>
        </tr>
        <tr>
          <td><label>Дата начала</label></td>
          <td><input type="date" v-model="project.startDate" /></td>
        </tr>
        <tr>
          <td><label>Дата завершения</label></td>
          <td><input type="date" v-model="project.endDate" /></td>
        </tr>
        <tr>
          <td><label>Приоритет</label></td>
          <td><input type="number" min="1" max="5" v-model="project.priority" placeholder="от 1 до 5" /></td>
        </tr>
        <tr>
          <td><label>Руководитель проекта</label></td>
          <td>
            <input type="text" v-model="filterManager" @input="searchManager" placeholder="Поиск..." />
            <ul v-if="managers.length && filterManager.trim() !== ''">
              <li v-for="emp in managers"
                  :key="emp.id"
                  @click="selectManager(emp)">
                <button>{{ emp.lastName }} {{ emp.firstName }} {{ emp.patronymic || '' }}</button>
              </li>
            </ul>
          </td>
        </tr>
        <tr>
          <td><label>Файлы проекта</label></td>
          <td>
            <div @dragover.prevent
                 @drop.prevent="handleDrop">
              Перетащите файлы сюда или кликните для выбора
              <input type="file" multiple @change="handleFiles" />
            </div>
            <ul>
              <li v-for="(document, index) in documents" :key="index">{{ document.name }}</li>
            </ul>
          </td>
        </tr>
        <tr>
          <td><label>Сотрудники</label></td>
          <td>
            <p>{{ selectedEmployees }}</p>
            <input type="text" v-model="filterEmployee" @input="searchEmployee" placeholder="Поиск..." />
            <ul v-if="employees.length && filterEmployee.trim() !== ''">
              <li v-for="emp in employees"
                  :key="emp.id"
                  @click="selectEmployee(emp)">
                <button>{{ emp.lastName }} {{ emp.firstName }} {{ emp.patronymic || '' }}</button>
              </li>
            </ul>
          </td>
        </tr>
      </tbody>
    </table>
  </form>
  <button @click="submit">Создать</button>
</template>

<script setup>
  import { ref, reactive } from 'vue';
  import { useRouter } from 'vue-router';
  import { createProject } from '../../api/projectService';
  import { getEmployees } from '../../api/employeeService';

  const router = useRouter();

  const selectedEmployees = ref('');
  const filterManager = ref('');
  const filterEmployee = ref('');

  const project = reactive({
    name: '',
    customer: '',
    performer: '',
    startDate: '',
    endDate: '',
    priority: '',
    managerId: '',
    employeeIds: []
  });

  const managers = ref([]);
  const employees = ref([]);
  const documents = ref([]);

  const searchManager = async () => {
    try {
      managers.value = await getEmployees(filterManager.value);
    } catch (err) {
      console.error('Ошибка загрузки сотрудников:', err);
    }
  };

  const searchEmployee = async () => {
    try {
      employees.value = await getEmployees(filterEmployee.value);
    } catch (err) {
      console.error('Ошибка загрузки сотрудников:', err);
    }
  };

  const selectManager = (emp) => {
    project.managerId = emp.id;
    filterManager.value = `${emp.lastName} ${emp.firstName}`;
    managers.value = [];
  };

  const selectEmployee = (emp) => {
    if (!project.employeeIds.includes(emp.id)) {
      project.employeeIds.push(emp.id);
      selectedEmployees.value += `, ${emp.lastName} ${emp.firstName}`;
    }
    filterEmployee.value = '';
    employees.value = [];
  };

  // Drag & drop
  const handleDrop = (event) => {
    documents.value.push(...event.dataTransfer.files);
  };

  const handleFiles = (event) => {
    documents.value.push(...event.target.files);
  };

  const submit = async () => {
    try {
      const formData = new FormData();
      formData.append('name', project.name);
      formData.append('customer', project.customer);
      formData.append('performer', project.performer);
      formData.append('startDate', project.startDate);
      formData.append('endDate', project.endDate);
      formData.append('priority', project.priority);
      formData.append('managerId', project.managerId);
      documents.value.forEach(document => formData.append('documents', document));
      project.employeeIds.forEach(emp => formData.append('employeeIds', emp));

      const id = await createProject(formData);
      router.push(`/Project/${id}`);
    } catch (err) {
      console.error('Ошибка создания проекта:', err);
    }
  };
</script>

