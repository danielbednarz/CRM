<template>
  <div class="column">
    <div class="row">
      <q-card square class="shadow-15">
        <q-form class="q-gutter-md" @submit="tryLogin">
          <q-card-section class="bg-primary q-px-md">
            <h4 class="text-h5 text-white q-my-sm">Logowanie</h4>
          </q-card-section>
          <q-card-section class="q-px-xl">
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
    </div>
  </div>
</template>

<script setup>
import { useAuthenticationStore } from "../stores/authentication";
import { useQuasar } from "quasar";

const authenticationStore = useAuthenticationStore();
const $q = useQuasar();

async function tryLogin() {
  try {
    await authenticationStore.login();
    // redirect("/");
    $q.notify({
      type: "info",
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
