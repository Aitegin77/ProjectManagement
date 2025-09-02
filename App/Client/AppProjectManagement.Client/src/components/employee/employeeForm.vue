<template>
  <form @submit.prevent="submit">
    <input type="text" v-model="employee.lastName" placeholder="Фамилия" />
    <input type="text" v-model="employee.firstName" placeholder="Имя" />
    <input type="text" v-model="employee.patronymic" placeholder="Отчество" />
    <input type="date" v-model="employee.mail" placeholder="Почта" />
    <button type="submit">Добавить</button>
  </form>
</template>

<script setup>
  import { reactive } from 'vue';
  import { useRouter } from 'vue-router';
  import { createEmployee } from '../../api/employeeService';

  const router = useRouter();

  const employee = reactive({
    lastName: '',
    firstName: '',
    patronymic: '',
    mail: ''
  });

  const submit = async () => {
    try {
      const id = await createEmployee(employee);
      router.push(`/Employee/${id}`);
    } catch (err) {
      console.error('Ошибка создания сотрудника:', err);
    }
  };
</script>
