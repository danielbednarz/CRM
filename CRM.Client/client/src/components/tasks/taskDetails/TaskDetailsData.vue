<template>
  <div class="q-pa-lg">
    <div class="row">
      <div class="col-md-9 col-xs-12 q-px-md">
        <q-card class="q-gutter-y-md">
          <q-form class="q-pa-xl q-gutter-md">
            <div class="row">
              <div class="col-md-12 q-px-md">
                <div class="text-h4">
                  Zadanie: {{ tasksStore.task.signature }}
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-md-6 col-xs-6 q-px-md">
                <q-input
                  label="Klient"
                  v-model="tasksStore.task.clientName"
                  readonly
                />
              </div>
              <div class="col-md-6 col-xs-6 q-px-md">
                <q-input
                  label="Pracownik przypisany"
                  v-model="tasksStore.task.assignedUser"
                  readonly
                />
              </div>
            </div>
            <div class="row">
              <div class="col-md-6 col-xs-6 q-px-md">
                <q-input label="Termin realizacji" v-model="myTime" readonly />
              </div>
              <div class="col-md-6 col-xs-6 q-px-md">
                <q-input
                  label="Osoba nadzorująca"
                  v-model="tasksStore.task.supervisor"
                  readonly
                />
              </div>
            </div>
            <div class="row">
              <div class="col-md-12 col-xs-12 q-px-md">
                <q-input
                  label="Opis zadania"
                  v-model="tasksStore.task.description"
                  autogrow
                  readonly
                />
              </div>
            </div>
            <div class="row"></div>
            <div class="row"></div>
          </q-form>
        </q-card>
      </div>
      <div class="col-md-3 col-xs-12 q-px-md">
        <q-stepper
          v-model="tasksStore.task.stepValue"
          vertical
          color="primary"
          animated
        >
          <q-step
            :name="-1"
            title="Rejestracja"
            icon="settings"
            :done="tasksStore.task.stepValue > -1"
          />
          <q-step
            :name="0"
            title="Zarejestrowano"
            icon="settings"
            :done="tasksStore.task.stepValue > 0"
          >
            <q-stepper-navigation>
              <q-btn
                @click="moveToNextStep()"
                color="primary"
                label="Do realizacji"
              />
              <q-btn
                flat
                @click="cancelTask()"
                color="negative"
                label="Anuluj"
                class="q-ml-sm"
              />
            </q-stepper-navigation>
          </q-step>

          <q-step
            :name="1"
            title="W realizacji"
            icon="create_new_folder"
            :done="tasksStore.task.stepValue > 1"
          >
            <q-stepper-navigation>
              <q-btn
                @click="moveToNextStep()"
                color="primary"
                label="Przekaż dalej"
              />
              <q-btn
                flat
                @click="moveToPreviousStep()"
                color="primary"
                label="Cofnij"
                class="q-ml-sm"
              />
            </q-stepper-navigation>
          </q-step>

          <q-step
            :name="2"
            title="Potwierdzenie wykonania"
            icon="assignment"
            :done="tasksStore.task.stepValue > 2"
          >
            <q-stepper-navigation>
              <q-btn
                @click="moveToNextStep()"
                color="primary"
                label="Przekaż dalej"
              />
              <q-btn
                flat
                @click="moveToPreviousStep()"
                color="primary"
                label="Cofnij"
                class="q-ml-sm"
              />
            </q-stepper-navigation>
          </q-step>

          <q-step
            :name="3"
            title="Zakończone"
            icon="add_comment"
            :done="tasksStore.task.stepValue > 3"
          >
            <q-stepper-navigation>
              <q-btn
                flat
                @click="moveToPreviousStep()"
                color="primary"
                label="Cofnij"
                class="q-ml-sm"
              />
            </q-stepper-navigation>
          </q-step>
          <q-step
            :name="4"
            title="Anulowane"
            active-icon="fa-solid fa-xmark"
            icon="fa-solid fa-xmark"
            :done="tasksStore.task.stepValue > 4"
          />
        </q-stepper>
      </div>
    </div>
  </div>
</template>
<script>
import { ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useTasksStore } from "../../../stores/tasks";
import { useQuasar } from "quasar";

export default {
  props: {
    task: {
      type: Object,
    },
  },
  setup(props) {
    const route = useRoute();
    const router = useRouter();
    const tasksStore = useTasksStore();
    const $q = useQuasar();

    return {
      props,
      tasksStore,
      myTime: ref(
        new Date(props.task.completionDate).toLocaleDateString("pl-PL")
      ),
      async moveToNextStep() {
        await tasksStore
          .moveToNextStep({
            taskId: this.props.task.id,
            requireConfirmation: false,
          })
          .then(() => {
            tasksStore.getTaskDetails(this.props.task.id);
          });
      },
      async moveToPreviousStep() {
        await tasksStore
          .moveToPreviousStep({
            taskId: this.props.task.id,
            requireConfirmation: false,
          })
          .then(() => {
            tasksStore.getTaskDetails(this.props.task.id);
          });
      },
      async cancelTask() {
        await tasksStore.cancelTask(this.props.task.id).then(() => {
          tasksStore.getTaskDetails(this.props.task.id);
        });
      },
    };
  },
};
</script>
