<script setup lang="ts">

const message = reactive({
  name: "",
  startDate: "",
  endDate: "",
});

const ok = ref(false);
const error = ref("");

async function createJourney() {
  // TODO: fix fetch
  console.log("create journey");
  // const res = await fetch({
  //     url: "http://localhost:8080/journeys/api/journeys",
  //     formData: message,
  // },{
  //     data: message
  // });

}

</script>

<template>
  <section id="contact" class=" flex justify-center">
    <div class="py-6 md:py-24 w-full max-w-6xl mx-6 flex flex-col justify-center items-center">
      <div id="contact-form-submitted"
        class="font-medium mx-auto max-w-lg flex flex-col items-center text-center motion-safe:animate-fadeIn"
        :class="{ hidden: !ok }">
        <h2 class="text-3xl">Danke für Ihr Interesse!</h2>
        <h3 class=" text-xl">Wir haben Ihre Anfrage erhalten und kümmern uns darum.</h3>
        <Icon name="lucide:check-circle" class="w-24 h-24 text-primary" />
      </div>
      <div id="contact-form-container" class="mx-3" :class="{ hidden: ok }">
        <h2 class=" text-3xl font-medium mb-3">Create journey</h2>
        <form id="contact-form" class="w-full max-w-lg" @submit.prevent="createJourney()">
          <div class="flex flex-wrap mb-6">
            <label class="block uppercase tracking-wide  text-xs font-bold mb-2" for="contact-lastname">
              Name
            </label>
            <input id="contact-name" v-model="message.startDate" required
              class="appearance-none block w-full border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus: focus:border-primary"
              type="text" />
          </div>

          <div class="flex flex-wrap mb-6">
            <label class="block uppercase tracking-wide  text-xs font-bold mb-2" for="contact-lastname">
              From
            </label>
            <input id="contact-name" v-model="message.name" required
              class="appearance-none block w-full   border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus: focus:border-primary"
              type="date" />
          </div>

          <div class="flex flex-wrap mb-6">
            <label class="block uppercase tracking-wide  text-xs font-bold mb-2" for="contact-lastname">
              To
            </label>
            <input id="contact-name" v-model="message.endDate" required
              class="appearance-none block w-full   border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus: focus:border-primary"
              type="date" />
          </div>

          <button type="submit" class="bg-green-600 p-2 rounded-md  w-full">Create</button>
        </form>
      </div>
    </div>
  </section>
</template>
