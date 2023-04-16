<template>
  <div class="q-pa-lg">
    <div class="row">
      <div class="col-md-9 col-xs-12 q-px-md">
        <q-card class="q-gutter-y-md">
          <q-form class="q-pa-xl q-gutter-md" ref="myForm">
            <div class="row">
              <div class="col-md-12 q-px-md">
                <div class="text-h4">Stwórz nowe zadanie</div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-12 col-xs-12 q-px-md">
                <q-select
                  label="Klient"
                  v-model="taskToAdd.clientId"
                  :options="clientsStore.clients"
                  option-value="id"
                  option-label="name"
                  emit-value
                  map-options
                />
              </div>
            </div>
            <div class="row q-pt-md">
              <div class="col-md-4 col-xs-12 q-px-md">
                <q-select
                  label="Priorytet"
                  v-model="taskToAdd.priority"
                  :options="priorityValues"
                  option-value="value"
                  option-label="name"
                  emit-value
                  map-options
                  :rules="[(val) => !!val || 'Pole nie może być puste']"
                />
              </div>
              <div class="col-md-2 col-xs-12 q-px-md q-pt-md">
                <q-checkbox
                  v-model="taskToAdd.requireConfirmation"
                  label="Wymagane potwierdzenie?"
                />
              </div>
              <div class="col-md-6 col-xs-12 q-px-md">
                <q-select
                  label="Osoba przypisana"
                  v-model="taskToAdd.assignedUserId"
                  :options="tasksStore.users"
                  option-value="id"
                  option-label="name"
                  emit-value
                  map-options
                  :rules="[(val) => !!val || 'Pole nie może być puste']"
                />
              </div>
            </div>
            <div class="row">
              <div class="col-md-6 col-xs-6 q-px-md">
                <q-input
                  class="date-input"
                  label="Termin realizacji"
                  type="date"
                  v-model="taskToAdd.completionDate"
                  placeholder=""
                />
              </div>
              <div class="col-md-6 col-xs-6 q-px-md">
                <q-select
                  label="Osoba nadzorująca"
                  v-model="taskToAdd.supervisorId"
                  :options="tasksStore.users"
                  option-value="id"
                  option-label="name"
                  emit-value
                  map-options
                />
              </div>
            </div>
            <div class="row">
              <div class="col-md-12 col-xs-12 q-px-md">
                <q-input
                  label="Opis zadania"
                  v-model="taskToAdd.description"
                  autogrow
                  style="font-size: 20px;"
                  maxlength="1024"
                  clearable
                  :rules="[(val) => !!val || 'Pole nie może być puste']"
                />
              </div>
            </div>
          </q-form>
        </q-card>
      </div>
      <div class="col-md-3 col-xs-12 q-px-md">
        <q-stepper v-model="stepValue" vertical color="primary" animated>
          <q-step :name="-1" title="Rejestracja" icon="settings">
            Kliknięcie poniższego przycisku spowoduje zarejestrowanie zadanie
            oraz powiadomienia mailowego osoby przypisanej oraz osoby
            nadzorującej.
            <q-stepper-navigation>
              <q-btn
                @click="addTask()"
                type="submit"
                color="primary"
                label="Zarejestruj zadanie"
              />
            </q-stepper-navigation>
          </q-step>
          <q-step
            :name="0"
            title="Zarejestrowano"
            icon="settings"
            :done="false"
          >
          </q-step>

          <q-step
            :name="1"
            title="W realizacji"
            icon="create_new_folder"
            :done="false"
          >
          </q-step>

          <q-step
            :name="2"
            title="Potwierdzenie wykonania"
            icon="assignment"
            :done="false"
          >
          </q-step>

          <q-step :name="3" title="Zakończone" icon="add_comment" :done="false">
          </q-step>
          <q-step
            :name="4"
            title="Anulowane"
            active-icon="fa-solid fa-xmark"
            icon="fa-solid fa-xmark"
            :done="false"
          />
        </q-stepper>
      </div>
    </div>
  </div>
</template>
<script>
import { ref, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useTasksStore } from "../../stores/tasks";
import { useClientsStore } from "../../stores/clients";
import { useQuasar } from "quasar";

export default {
  setup() {
    const tasksStore = useTasksStore();
    const clientsStore = useClientsStore();
    const router = useRouter();
    const route = useRoute();
    const $q = useQuasar();
    const myForm = ref(null);

    let priorityValues = [
      {
        value: 1,
        name: "Niski",
      },
      {
        value: 2,
        name: "Średni",
      },
      {
        value: 3,
        name: "Wysoki",
      },
      {
        value: 4,
        name: "Krytyczny",
      },
    ];
    let currentDate = new Date();
    let taskToAdd = ref({
      description: "",
      assignedUserId: null,
      supervisorId: null,
      clientId: null,
      completionDate: currentDate.toLocaleDateString("fr-CA"),  // dla uzyskania daty dd.MM.yyyy
      priority: priorityValues[1].value,
      requireConfirmation: false
    });

    onMounted(() => clientsStore.getClients());
    onMounted(() => tasksStore.getUsersToSelect());

    return {
      myForm,
      tasksStore,
      clientsStore,
      router,
      route,
      taskToAdd,
      priorityValues,
      stepValue: -1,
      async addTask() {
        let success = await myForm.value.validate();
        if (success) {
          await tasksStore.addTask(this.taskToAdd).then(response => {
            if (response.status == 200) {
              $q.notify({
                type: "info",
                message: `Zadanie zarejestrowane pomyślnie`,
              });
              router.push(`/tasks/${response.data}`);
            }
            else {
              $q.notify({
                type: "negative",
                message: `Błąd przy próbie rejestracji zadania`,
              });
            }
          });
        }
      },
    };
  },
};
</script>
<style scoped>
  .q-select {
  font-size: 20px;
}
.date-input {
  cursor: pointer;
}
</style>
