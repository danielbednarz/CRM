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
              <div class="col-md-12 col-xs-12 q-px-md">
                <q-input
                  label="Klient"
                  v-model="tasksStore.task.clientName"
                  readonly
                />
              </div>
            </div>
            <div class="row">
              <div class="col-md-4 col-xs-12 q-px-md">
                <q-select
                  label="Priorytet"
                  v-model="tasksStore.task.priority"
                  readonly
                  option-value="value"
                  option-label="name"
                  emit-value
                  map-options
                  :rules="[(val) => !!val || 'Pole nie może być puste']"
                />
              </div>
              <div class="col-md-2 col-xs-12 q-px-md q-pt-md">
                <q-checkbox
                  v-model="tasksStore.task.requireConfirmation"
                  label="Wymagane potwierdzenie?"
                  disable
                />
              </div>
              <div class="col-md-6 col-xs-6 q-px-md">
                <q-input
                  label="Osoba przypisana"
                  v-model="tasksStore.task.assignedUser"
                  readonly
                />
              </div>
            </div>
            <div class="row">
              <div class="col-md-6 col-xs-12 q-px-md">
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
            Na tym etapie, zadanie jest w trakcie realizacji. Po jego ukończeniu
            przekaż zadanie do kolejnego kroku.
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
            Potwierdzenie poprawności wykonanego zadania. W przypadku braku
            uwag, należy zakończyć zadanie poprzez przycisk 'Przekaż dalej'.
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
            Zadanie jest zakończone. W uzasadnionych przypadkach, można zwrócić
            zadanie do poprzedniego kroku.
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

    <div class="row q-pt-lg">
      <div class="col-md-9 col-xs-12 q-px-md">
        <q-card class="q-gutter-y-md">
          <q-card-section>
            <div class="text-h5 q-pt-md q-pl-md">
              <span>Komentarze ({{ tasksStore.task.comments.length }})</span>
              <q-btn
                flat
                class="q-pb-sm"
                color="secondary"
                icon="fa-solid fa-pen-to-square"
                @click="addCommentDialogVisible = true"
              ></q-btn>
            </div>
          </q-card-section>
          <div class="q-px-md row justify-center">
            <div style="width: 100%">
              <q-virtual-scroll
                class="q-virtual-scroll-container"
                separator
                :items="tasksStore.task.comments"
                v-slot="{ item, index }"
                hide-scrollbar
              >
                <q-chat-message
                  class="chat-message q-px-md"
                  bg-color="tertiary"
                  :key="index"
                  :text="[`${item.content}`]"
                  :stamp="formatDate(item.createdDate)"
                  sent
                  size="12"
                >
                  <template v-slot:name>
                    <span class="comment-author">{{ item.createdBy }}</span
                    ><q-chip
                      class="q-mb-sm q-ml-sm"
                      clickable
                      @click="setCommentToDelete(item.id)"
                      color="negative"
                      size="sm"
                      text-color="white"
                      icon="fa-solid fa-trash"
                      >Usuń</q-chip
                    >
                  </template>
                </q-chat-message>
              </q-virtual-scroll>
            </div>
          </div>
        </q-card>
      </div>
    </div>
  </div>

  <q-dialog v-model="addCommentDialogVisible">
    <q-card style="min-width: 400px; max-height: 400px">
      <q-card-section>
        <div class="text-h6">Dodaj komentarz</div>
      </q-card-section>

      <q-card-section class="q-pt-none">
        <q-input
          style="font-size: 16px"
          label="Treść komentarza"
          v-model="commentToAdd.content"
          maxlength="1024"
          autofocus
          autogrow
          clearable
        />
      </q-card-section>

      <q-card-actions align="right" class="text-primary">
        <q-btn flat label="Anuluj" v-close-popup />
        <q-btn flat label="Dodaj" @click="addComment()" />
      </q-card-actions>
    </q-card>
  </q-dialog>

  <q-dialog v-model="deleteCommentDialogVisible">
    <q-card>
      <q-card-section class="row items-center">
        <q-icon
          name="fa-solid fa-triangle-exclamation"
          color="primary"
          size="lg"
        />
        <span class="q-ml-sm" style="font-size: 18px"
          >Czy na pewno chcesz usunąć ten komentarz?</span
        >
      </q-card-section>

      <q-card-actions align="right">
        <q-btn flat label="Anuluj" color="primary" v-close-popup />
        <q-btn
          flat
          label="Tak"
          color="negative"
          @click="deleteComment()"
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>
<script>
import { ref, computed, reactive } from "vue";
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
      commentIdToDelete: ref(null),
      addCommentDialogVisible: ref(false),
      deleteCommentDialogVisible: ref(false),
      commentToAdd: ref({
        userTaskId: props.task.id,
        content: "",
      }),
      myTime: ref(
        new Date(props.task.completionDate).toLocaleDateString("pl-PL")
      ),
      formatDate(date) {
        const options = {
          day: "2-digit",
          month: "2-digit",
          year: "numeric",
          hour: "2-digit",
          minute: "2-digit",
        };
        return new Date(date).toLocaleDateString("pl-PL", options);
      },
      async moveToNextStep() {
        await tasksStore.moveToNextStep(this.props.task.id).then(() => {
          tasksStore.getTaskDetails(this.props.task.id);
        });
      },
      async moveToPreviousStep() {
        await tasksStore.moveToPreviousStep(this.props.task.id).then(() => {
          tasksStore.getTaskDetails(this.props.task.id);
        });
      },
      async cancelTask() {
        await tasksStore.cancelTask(this.props.task.id).then(() => {
          tasksStore.getTaskDetails(this.props.task.id);
        });
      },
      async addComment() {
        let result = await tasksStore.addComment(this.commentToAdd);
        if (result.status == 200) {
          $q.notify({
            type: "info",
            message: `Komentarz dodany pomyślnie`,
          });
          this.addCommentDialogVisible = false;
          tasksStore.getTaskDetails(this.props.task.id);
        } else {
          $q.notify({
            type: "negative",
            message: `Komentarz nie został dodany`,
          });
        }
      },
      setCommentToDelete(commentId) {
        this.deleteCommentDialogVisible = true;
        this.commentIdToDelete = commentId;
      },
      async deleteComment() {
        tasksStore.deleteComment(this.commentIdToDelete).then((result) => {
          if (result.status == 200) {
            $q.notify({
              type: "info",
              message: `Komentarz usunięto pomyślnie`,
            });
            this.deleteCommentDialogVisible = false;
            tasksStore.getTaskDetails(this.props.task.id);
          } else {
            $q.notify({
              type: "negative",
              message: `Komentarz nie został usunięty`,
            });
          }
        });
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
.q-virtual-scroll-container {
  max-height: 300px;
  overflow-y: auto;
  padding-right: 10px;
}
.chat-message {
  font-size: 16px;
}
.comment-author {
  font-size: 18px;
}
</style>
