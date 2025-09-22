<script setup lang="ts">
import * as z from 'zod'
import { reactive, shallowRef, watch } from 'vue'
import type { FormSubmitEvent } from '@nuxt/ui'
import { CalendarDate, DateFormatter, getLocalTimeZone } from '@internationalized/date'

const schema = z.object({
  name: z.string(),
  startDate: z.date(),
  endDate: z.date(),
})

type JourneySchema = z.output<typeof schema>

const state = reactive<Partial<JourneySchema>>({
  name: undefined,
  startDate: undefined,
  endDate: undefined,
})

const startModel = shallowRef(undefined as CalendarDate | undefined)
const endModel = shallowRef(undefined as CalendarDate | undefined)

watch(startModel, (val) => (state.startDate = val?.toDate(getLocalTimeZone())))
watch(endModel, (val) => (state.endDate = val?.toDate(getLocalTimeZone())))

const df = new DateFormatter('en-US', { dateStyle: 'medium' })

const toast = useToast()

async function onSubmit(event: FormSubmitEvent<JourneySchema>) {

  let res = await fetch('/api/journeys', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(event.data),
  });

  toast.add({
    title: 'Success',
    description: 'The journey has been created.',
    color: 'success'
  });
}
</script>

<template>
  <div class="flex justify-center items-center min-h-screen ">
    <div class="w-full max-w-md p-6 rounded-lg shadow space-y-4">
      <UForm :schema="schema" :state="state" class="space-y-4" @submit="onSubmit">
        <UFormField label="Name" name="name" class="w-full">
          <UInput v-model="state.name" class="w-full" />
        </UFormField>

        <UFormField label="Start Date" name="startDate" class="w-full">
          <UPopover class="w-full">
            <UButton color="neutral" variant="subtle" icon="i-lucide-calendar" class="w-full text-left">
              {{ startModel ? df.format(startModel.toDate(getLocalTimeZone())) : 'Select a date' }}
            </UButton>
            <template #content>
              <UCalendar v-model="startModel" class="p-2" />
            </template>
          </UPopover>
        </UFormField>

        <UFormField label="End Date" name="endDate" class="w-full">
          <UPopover class="w-full">
            <UButton color="neutral" variant="subtle" icon="i-lucide-calendar" class="w-full text-left">
              {{ endModel ? df.format(endModel.toDate(getLocalTimeZone())) : 'Select a date' }}
            </UButton>
            <template #content>
              <UCalendar v-model="endModel" class="p-2" />
            </template>
          </UPopover>
        </UFormField>

        <UButton type="submit">Submit</UButton>
      </UForm>
    </div>
  </div>
</template>
