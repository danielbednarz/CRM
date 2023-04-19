<template>
  <div class="q-pa-lg">
    <div class="q-gutter-y-md">
      <q-card>
        <q-form @submit="onSubmit(userToAdd)" class="q-pa-xl q-gutter-sm">
          <div class="text-h4 q-ml-lg q-mb-md">Zarejestruj użytkownika</div>
          <div class="row">
            <div class="col-md-4 col-xs-12 q-px-md">
              <q-input
                v-model="userToAdd.FirstName"
                label="Imię"
                :rules="[
                  (val) => val.length > 1 || 'Imię jest za krótkie',
                  (val) => val.length < 30 || 'Imię jest za długie',
                ]"
              />
            </div>
            <div class="col-md-4 col-xs-12 q-px-md">
              <q-input
                v-model="userToAdd.LastName"
                label="Nazwisko"
                :rules="[
                  (val) => val.length > 1 || 'Nazwisko jest za krótkie',
                  (val) => val.length < 50 || 'Nazwisko jest za długie',
                ]"
              />
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 col-xs-12 q-px-md">
              <q-input
                v-model="userToAdd.Email"
                label="Email"
                type="email"
                :rules="emailRules"
              >
              <q-tooltip>
                Na podany email zostanie wysłana wiadomość aktywacyjna
              </q-tooltip>
              </q-input>
            </div>
            <div class="col-md-4 col-xs-12 q-px-md">
              <q-input v-model="userToAdd.PhoneNumber" label="Numer telefonu" />
            </div>
          </div>
          <div class="row q-pb-md">
            <div class="col-xs-12 q-pl-md q-pt-md q-gutter-md">
              <q-btn
                color="primary"
                label="Zarejestruj użytkownika"
                type="submit"
              ></q-btn>
              <q-btn
                color="secondary"
                label="Anuluj"
                @click="$router.go(-1)"
              ></q-btn>
            </div>
          </div>
        </q-form>
      </q-card>
    </div>
  </div>
</template>
<script>
import { ref, onMounted, watch } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useAuthenticationStore } from "../../stores/authentication";
import { useQuasar } from "quasar";

export default {
  setup() {
    const authenticationStore = useAuthenticationStore();
    const route = useRoute();
    const router = useRouter();
    const quasar = useQuasar();

    let userToAdd = ref({
      FirstName: "",
      LastName: "",
      Email: "",
      PhoneNumber: "",
      Password: "",
    });

    function checkIfEmail(str) {
      const regexExp =
        /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/gi;

      return regexExp.test(str);
    }

    return {
      emailRules: [
        (val) => (val !== null && val !== "") || "Pole nie może być puste",
        (val) => checkIfEmail(val) || "Wartość nie jest adresem email",
      ],
      authenticationStore,
      route,
      router,
      quasar,
      userToAdd,
      async onSubmit() {
        await authenticationStore.register(this.userToAdd).then((result) => {
          if (result.status == 200) {
            quasar.notify({
              type: "info",
              message: `Użytkownik dodany pomyślnie`,
            });
            router.go(-1);
          } else {
            quasar.notify({
              type: "negative",
              message: `Błąd przy próbie dodania użytkownika`,
            });
          }
        });
      },
    };
  },
};
</script>
