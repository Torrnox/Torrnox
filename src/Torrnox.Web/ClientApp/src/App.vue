<template>
  <div class="flex h-screen">
    <Sidebar />
    <div class="flex flex-col flex-1">
      <Header />
      <main class="p-4 overflow-auto">
        <router-view />
      </main>
    </div>
  </div>
</template>

<script setup lang="ts">
import Sidebar from './components/Sidebar.vue'
import Header from './components/Header.vue'
</script>