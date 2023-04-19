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
      <q-page class="q-pa-md row justify-center items-center">
        <q-card square class="shadow-15 col-md-4 col-xs-12">
          <q-card-section class="bg-primary">
            <div class="text-h5 text-white q-my-sm">
              Potwierdź rejestrację konta
            </div>
          </q-card-section>
          <q-card-section>
            <q-input
              square
              clearable
              v-model="password"
              type="password"
              label="Hasło"
              :rules="[
                (val) =>
                  val.length >= 8 || 'Hasło musi mieć co najmniej 8 znaków',
                  val => hasUpperCase || 'Hasło musi mieć co najmniej jedną dużą literę'
              ]"
            />
            <q-input
              square
              clearable
              v-model="confirmPassword"
              type="password"
              label="Powtórz hasło"
              :rules="[
                (val) =>
                  val.length >= 8 || 'Hasło musi mieć co najmniej 8 znaków',
                  val => hasUpperCase || 'Hasło musi mieć co najmniej jedną dużą literę'
              ]"
            />
          </q-card-section>
          <q-card-section class="row">
            <q-btn
              color="primary"
              class="col-md-12"
              @click="confirmAccount()"
              :disable="!passwordsMatch || password.length == 0"
              >Potwierdź rejestrację</q-btn
            >
          </q-card-section>
        </q-card>
      </q-page>
    </q-page-container>
  </q-layout>
</template>

<script>
import { onMounted, ref, computed } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useAuthenticationStore } from "../stores/authentication";
import { useQuasar } from "quasar";

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    const authenticationStore = useAuthenticationStore();
    const $q = useQuasar();
    const info = ref("");
    const description = ref("");
    const userId = route.query.id;
    const token = route.query.token;
    const password = ref("");
    const confirmPassword = ref("");

    const passwordsMatch = computed(() => {
      return password.value === confirmPassword.value;
    });

    const hasUpperCase = computed(() => {
      return /[A-Z]/.test(password.value);
    });

    return {
      route,
      authenticationStore,
      info,
      description,
      userId,
      token,
      password,
      confirmPassword,
      passwordsMatch,
      hasUpperCase,
      confirmAccount() {
        authenticationStore
          .confirmEmail({
            userId: this.userId,
            token: this.token,
            password: this.password,
          })
          .then((result) => {
            if (result.status == 200) {
              $q.notify({
                type: "positive",
                message: "Konto zostało potwierdzone",
              });
              router.push("/");
            } else {
              $q.notify({
                type: "negative",
                message: "Błąd przy próbie potwierdzenia konta",
              });;
            }
          });
      },
    };
  },
};
</script>
