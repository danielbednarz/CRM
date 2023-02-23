<template>
  <q-card square class="shadow-15 col-md-3 col-xs-8">
    <q-form class="q-gutter-md" @submit="tryLogin">
      <q-card-section class="bg-primary">
        <h4 class="text-h5 text-white q-my-sm">Logowanie</h4>
      </q-card-section>
      <q-card-section class="">
        <q-input
          square
          clearable
          v-model="authenticationStore.form.email"
          type="email"
          label="E-mail"
        />
        <q-input
          square
          clearable
          v-model="authenticationStore.form.password"
          type="password"
          label="Hasło"
        />
      </q-card-section>
      <q-card-actions>
        <q-btn
          unelevated
          color="secondary"
          size="lg"
          class="full-width"
          label="Zaloguj się"
          type="submit"
        />
      </q-card-actions>
    </q-form>
  </q-card>
</template>

<script setup>
import { useAuthenticationStore } from "../stores/authentication";
import { useQuasar } from "quasar";

const authenticationStore = useAuthenticationStore();
const $q = useQuasar();

async function tryLogin() {
  try {
    await authenticationStore.login();
    $q.notify({
      type: "positive",
      message: `Zalogowano pomyślnie`,
    });
  } catch (ex) {
    $q.notify({
      type: "negative",
      message: "Błędny login lub hasło",
    });
  }
}
</script>
