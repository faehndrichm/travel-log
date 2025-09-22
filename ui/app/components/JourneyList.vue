<script setup lang="ts">

const baseURL = "http://nginx:80"; // TODO: config.public.apiServerBase ????;
const { data, status, error, refresh } = await useFetch<any[]>(baseURL + '/journeys/api/journeys');
</script>

<template>
    <div>
        <h2 class="text-lg font-medium mb-4">Journeys</h2>
        <ul v-if="data">
            <li v-for="journey in data" :key="journey.id">
                {{ journey.name }} ({{ journey.startDate }} - {{ journey.endDate }})
            </li>
        </ul>
        <div v-else-if="error">
            Error loading journeys: {{ error.message }}
        </div>
        <div v-else>
            <UIcon name="i-lucide-loader-circle" class="size-5" />
            Loading journeys...
        </div>
    </div>
</template>