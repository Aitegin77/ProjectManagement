<template>
  <div>
    <router-link :to="`/Project`">Проекты</router-link>&emsp;
    <router-link :to="`/Employee`">Сотрудники</router-link>
    <div v-if="$route.path === '/'">
      <h1>Главная страница</h1>
      <p>Добро пожаловать!</p>
    </div>
    <router-view v-else :key="$route.fullPath" />
  </div>
</template>
