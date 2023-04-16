<template>
  <div class="q-pa-lg">
    <div class="q-gutter-y-md">
      <q-card>
        <q-form
          @submit="onSubmit"
          class="q-pa-xl q-gutter-sm"
          v-if="administrationStore.user"
        >
        <div class="text-h4 q-ml-lg q-mb-md">Szczegóły użytkownika</div>
        <div class="row">
          <div class="col-md-6 col-xs-12 q-px-md">
            <q-input
                v-model="administrationStore.user.firstName"
                label="Imię"
                :rules="[
                  (val) => val.length > 1 || 'Imię jest za krótkie',
                  (val) => val.length < 30 || 'Imię jest za długie',
                ]"
              />
          </div>
          <div class="col-md-6 col-xs-12 q-px-md">
            <q-input
                v-model="administrationStore.user.lastName"
                label="Nazwisko"
                :rules="[
                  (val) => val.length > 1 || 'Nazwisko jest za krótkie',
                  (val) => val.length < 50 || 'Nazwisko jest za długie',
                ]"
              />
          </div>
        </div>
        <div class="row">
          <div class="col-md-6 col-xs-12 q-px-md">
            <q-input
                v-model="administrationStore.user.email"
                label="Email"
                type="email"
                :rules="emailRules"
              />
          </div>
          <div class="col-md-4 col-xs-12 q-px-md">
            <q-input
                v-model="administrationStore.user.phoneNumber"
                label="Numer telefonu"
                :rules="[
                  (val) => val.length > 6 || 'Numer jest za krótki',
                  (val) => val.length < 15 || 'Numer jest za długi',
                ]"
              />
          </div>
          <div class="col-md-2 col-xs-12">
            <q-checkbox
              v-model="administrationStore.user.isActive"
              class="q-mt-lg q-ml-md"
              label="Czy aktywny?"
            />
          </div>
        </div>
        <div class="row">
          <div class="col-md-12 q-px-md">
            <q-table
              style="margin-left: -4px;"
              grid
              title="Grupy"
              :rows="administrationStore.user.roles"
              :columns="columns"
              :hide-pagination="true"
            >
            <template v-slot:top>
              <div class="text-h6" style="margin-left: -10px;">Grupy</div>
            </template>
            <template v-slot:body="props">
              <q-tr :props="props">
                <q-td key="name" :props="props">
                    {{ props.row.name }}
                </q-td>
              </q-tr>
            </template>
            </q-table>
          </div>
        </div>
        <div class="row q-pb-md">
          <div class="col-xs-12 q-pl-md q-pt-md q-gutter-md">
            <q-btn color="primary" label="Zapisz" type="submit"></q-btn>
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
import { useAdministrationStore } from "../../stores/administration";
import { useQuasar } from 'quasar'

export default {
  setup() {
    const administrationStore = useAdministrationStore();
    const route = useRoute();
    const router = useRouter();
    const quasar = useQuasar();
    onMounted(() => administrationStore.getUserDetails(route.params.id));

    function checkIfEmail(str) {
      const regexExp = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/gi;

      return regexExp.test(str);
    }

    const columns = [
      {
        name: 'Nazwa grupy', align: 'center', label: "Nazwa grupy", field: "name"
      }
    ]

    return {
      tab: ref("client"),
      emailRules: [
        val => (val !== null && val !== '') || 'Pole nie może być puste',
        val => checkIfEmail(val) || "Wartość nie jest adresem email"
      ],
      administrationStore,
      route,
      router,
      quasar,
      columns,
      initialPagination: {
        page: 1,
        rowsPerPage: 0
      },
      async onSubmit() {
        await administrationStore.editUser().then(
          () => {
            quasar.notify({
              type: "info",
              message: `Zapisano pomyślnie`,
            });
            router.go(-1);
          },
          (error) => {
            quasar.notify({
              type: "negative",
              message: `Błąd przy próbie zapisu`,
            });
          }
        );
      }
    };
  },
};
</script>
