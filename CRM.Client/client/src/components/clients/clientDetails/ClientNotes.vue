<template>
  <div class="q-px-lg q-pb-md">
    <q-timeline color="secondary">
      <q-timeline-entry heading body="Wydarzenia">
        <q-btn
          flat
          class="q-ml-md q-mb-sm"
          color="secondary"
          icon-right="fa-solid fa-plus"
          @click="addNoteDialogVisible = true"
        ></q-btn>
      </q-timeline-entry>
      <q-timeline-entry v-if="clientNotesStore.notes.length == 0">
        <div  class="text-body1">
          Brak wydarzeń
        </div>
      </q-timeline-entry>
      <q-timeline-entry
        v-for="(item, index) in clientNotesStore.notes"
        :key="index"
        :title="item.title"
        :subtitle="getSubtitle(item)"
      >
        <div class="text-body1">
          {{ item.content }}
        </div>
      </q-timeline-entry>
    </q-timeline>
  </div>

  <q-dialog v-model="addNoteDialogVisible" persistent>
    <q-card style="min-width: 350px; max-height: 500px;" >
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
        <q-btn
          flat
          label="Dodaj"
          @click="addNote()"
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
import { useQuasar } from "quasar";

export default {
  props: {},
  setup(props) {
    const route = useRoute();
    const router = useRouter();
    const clientNotesStore = useClientNotesStore();
    const $q = useQuasar();

    onMounted(() => clientNotesStore.getClientNotes(route.params.id));

    return {
      clientNotesStore,
      addNoteDialogVisible: ref(false),
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

      }
    };
  },
};
</script>
<style scoped></style>
