<template>
  <q-layout view="hHh lpR fFf">
    <q-header elevated class="bg-primary text-white" height-hint="98">
      <q-toolbar>

        <q-toolbar-title>
          <q-avatar>
            <img src="https://cdn.quasar.dev/logo-v2/svg/logo-mono-white.svg" />
          </q-avatar>
          CRM
        </q-toolbar-title>
      </q-toolbar>
    </q-header>
    <q-page-container>
      <q-page class="q-pa-md">
        <div class="q-px-xl">

            <h2 v-if="info">{{info}}</h2>
            <h6 v-if="description">{{description}}</h6>
            <q-btn color="primary" :to="{ name: 'home' }">Przejdź do strony głównej</q-btn>
        </div>
      </q-page>
    </q-page-container>
  </q-layout>

</template>

<script>
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import { useAuthenticationStore } from "../stores/authentication";

export default {
  setup() {
    const route = useRoute();
    const authenticationStore = useAuthenticationStore();
    const info = ref("");
    const description = ref("");

    onMounted(() => {
      debugger;
      const userId = route.query.id;
      const token = route.query.token;
      authenticationStore.confirmEmail(userId, token).then((result) => {
        if (result.status == 200) {
          info.value = "Konto zostało potwierdzone";
          description.value = "Możesz korzystać z systemu.";
        } else {
          info.value = "Błąd przy próbie potwierdzenia konta";
          description.value = "W celu rozwiązania problemu skontaktuj się z administratorem systemu.";
        }
      });
    });

    return {
      route,
      authenticationStore,
      info,
      description
    };
  },
};
</script>
