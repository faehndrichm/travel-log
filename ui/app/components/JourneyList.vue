<script setup lang="ts">
import { format } from 'date-fns';

const baseURL = "http://nginx:80"; // TODO: config.public.apiServerBase ????;
const { data, status, error, refresh } = await useFetch<any[]>(baseURL + '/journeys/api/journeys');
</script>

<template>
    <div>
        <div class="mx-auto max-w-md">
            <h2 class="text-lg font-medium mb-4 ">Journeys</h2>
            <ul class="divide-y" v-if="data">
                <li v-for="journey in data" :key="journey.id" class="py-2 flex flex-row justify-between">
                    <div class="">
                        <div class="text-lg font-bold">{{ journey.name }} - {{ journey.id }}</div>
                        <div>from {{ format(journey.startDate, "PP") }}</div>
                        <div>to {{ format(journey.endDate, "PP") }}</div>
                    </div>
                    <div class="flex gap-2 flex-col">
                        <UButton color="neutral" size="sm" icon="i-lucide-eye">View</UButton>
                        <UButton color="neutral" size="sm" icon="i-lucide-pen">Edit</UButton>
                    </div>
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
    </div>
</template>