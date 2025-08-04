<template>
  <aside class="w-64 bg-gray-800 border-r border-gray-700 p-4 flex flex-col">
    <h1 class="text-xl font-bold mb-6">MyApp</h1>
    <nav class="flex flex-col gap-2">
      <RouterLink
          v-for="item in navItems"
          :key="item.path"
          :to="item.path"
          class="py-2 px-3 rounded hover:bg-gray-700"
          :class="{ 'bg-gray-700': $route.path === item.path }"
      >
        {{ item.label }}
      </RouterLink>
    </nav>
  </aside>
</template>

<script setup lang="ts">
const navItems = [
  { path: '/', label: 'Dashboard' },
  { path: '/settings', label: 'Settings' },
]
</script>