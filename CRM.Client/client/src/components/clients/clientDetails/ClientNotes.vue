<template>
  <div class="q-px-lg q-pb-md">
    <q-timeline color="secondary">
      <q-timeline-entry heading body="Wydarzenia">
        <q-btn
          v-if="
            currentUser.roles.includes($ADMIN) ||
            currentUser.roles.includes($SUPERVISOR)
          "
          flat
          class="q-ml-md q-mb-sm"
          color="secondary"
          icon-right="fa-solid fa-plus"
          @click="addNoteDialogVisible = true"
        ></q-btn>
      </q-timeline-entry>
      <q-timeline-entry v-if="clientNotesStore.notes.length == 0">
        <div class="text-body1">Brak wydarzeń</div>
      </q-timeline-entry>

      <q-timeline-entry
        v-for="(item, index) in clientNotesStore.notes"
        :key="index"
        :title="item.title"
        :subtitle="getSubtitle(item)"
      >
        <q-btn
          v-if="
            currentUser.roles.includes($ADMIN) ||
            currentUser.roles.includes($SUPERVISOR)
          "
          class="delete-note-btn"
          flat
          size="md"
          color="negative"
          icon="fa-solid fa-minus"
          @click="setNoteToDelete(item.id)"
          ><q-tooltip> Usuń wydarzenie </q-tooltip></q-btn
        >
        <div class="text-body1">
          {{ item.content }}
        </div>
      </q-timeline-entry>
    </q-timeline>
  </div>

  <q-dialog v-model="addNoteDialogVisible" persistent>
    <q-card style="min-width: 350px; max-height: 500px">
      <q-card-section>
        <div class="text-h6">Dodaj wydarzenie</div>
      </q-card-section>

      <q-card-section class="q-pt-none">
        <q-input
          label="Tytuł"
          v-model="clientNotesStore.note.title"
          maxlength="100"
          autofocus
          @keyup.enter="addNoteDialogVisible = false"
        />
        <q-input
          label="Treść"
          maxlength="1024"
          v-model="clientNotesStore.note.content"
          autogrow
        />
      </q-card-section>

      <q-card-actions align="right" class="text-primary">
        <q-btn flat label="Anuluj" v-close-popup />
        <q-btn flat label="Dodaj" @click="addNote()" v-close-popup />
      </q-card-actions>
    </q-card>
  </q-dialog>

  <q-dialog v-model="deleteNoteDialogVisible" persistent>
    <q-card>
      <q-card-section class="row items-center">
        <q-icon
          name="fa-solid fa-triangle-exclamation"
          color="primary"
          size="lg"
        />
        <span class="q-ml-sm" style="font-size: 18px"
          >Czy na pewno chcesz usunąć to wydarzenie?</span
        >
      </q-card-section>

      <q-card-actions align="right">
        <q-btn flat label="Anuluj" color="primary" v-close-popup />
        <q-btn
          flat
          label="Tak"
          color="negative"
          @click="deleteNote()"
          v-close-popup
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>
<script>
import { ref, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useClientNotesStore } from "../../../stores/clientNotes";
import { useAuthenticationStore } from "../../../stores/authentication";
import { useQuasar } from "quasar";

export default {
  props: {},
  setup(props) {
    const route = useRoute();
    const router = useRouter();
    const clientNotesStore = useClientNotesStore();
    const $q = useQuasar();
    const currentUser = useAuthenticationStore().currentUser;

    onMounted(() => clientNotesStore.getClientNotes(route.params.id));

    return {
      clientNotesStore,
      addNoteDialogVisible: ref(false),
      deleteNoteDialogVisible: ref(false),
      currentUser,
      noteId: ref(null),
      getSubtitle(item) {
        let date = new Date(item.createdDate).toLocaleString("pl-PL");
        return `${date} - ${item.firstName} ${item.lastName}`;
      },
      async addNote() {
        clientNotesStore.note.clientId = route.params.id;
        let res = await clientNotesStore.addNote();
        if (res.status == 200) {
          $q.notify({
            type: "info",
            message: `Wydarzenie dodane pomyślnie`,
          });
          clientNotesStore.note.title = "";
          clientNotesStore.note.content = "";
          clientNotesStore.getClientNotes(route.params.id);
        } else {
          $q.notify({
            type: "negative",
            message: `Wydarzenie nie zostało dodane`,
          });
        }
      },
      setNoteToDelete(noteId) {
        this.deleteNoteDialogVisible = true;
        this.noteId = noteId;
      },
      async deleteNote() {
        await clientNotesStore.deleteNote(this.noteId).then(
          () => {
            $q.notify({
              type: "info",
              message: `Klient usunięty pomyślnie`,
            });
            router.push(`/clients`);
          },
          (error) => {
            $q.notify({
              type: "negative",
              message: `Błąd przy próbie usunięcia`,
            });
          }
        );
      },
    };
  },
};
</script>
<style scoped>
.delete-note-btn {
  float: right;
}
</style>
