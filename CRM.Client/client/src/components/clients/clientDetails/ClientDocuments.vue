<template>
  <div class="row q-py-md">
    <div class="col-md-12">
      <q-btn
        outline
        class="q-ml-md"
        label="Dodaj dokument"
        color="primary"
        icon-right="fa-solid fa-file-import"
        @click="dialog = true"
      />
    </div>
  </div>

  <div class="row" v-if="clientDocumentsStore.documents.length === 0">
    <div class="col-md-12">
      <div class="text-h5 q-ml-md">Brak plików</div>
    </div>
  </div>
  <div class="row" v-if="clientDocumentsStore.documents.length > 0">
    <div class="col-md-2 col-xs-6 col-sm-4 q-px-md" v-for="(item, index) in clientDocumentsStore.documents"
    :key="index">
      <q-card class="my-card text-secondary q-my-md">
        <q-card-section class="fit row wrap justify-center items-center" style="margin-bottom: 0; padding-bottom: 0;">
          <div class="text-h2" ><i class="fa-solid fa-file"></i></div>
        </q-card-section>
        <q-card-section class="fit row wrap justify-center items-center">
          <div class="text-subtitle text-black" style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden;">{{item.name}}</div>
        </q-card-section>
        <q-separator dark />
        <q-card-actions align="right">
          <q-btn flat>Pobierz</q-btn>
          <q-btn flat class="text-red" @click="setDocumentToDelete(item.id)">Usuń</q-btn>
        </q-card-actions>
      </q-card>
    </div>
  </div>

  <q-dialog v-model="dialog">
    <q-card>
      <q-uploader
        :url="getUrl"
        label="Dodaj dokumenty"
        multiple
        max-files="10"
        style="width: 320px; height: 250px;"
        @rejected="onRejected"
        @uploaded="onUploaded"
        :form-fields="[
          {
            name: 'clientId',
            value: route.params.id,
          },
        ]"
      />
    </q-card>
  </q-dialog>
  <q-dialog v-model="deleteDocumentDialogVisible" persistent>
    <q-card>
      <q-card-section class="row items-center">
        <q-icon
          name="fa-solid fa-triangle-exclamation"
          color="primary"
          size="lg"
        />
        <span class="q-ml-sm" style="font-size: 18px"
          >Czy na pewno chcesz usunąć ten plik?</span
        >
      </q-card-section>

      <q-card-actions align="right">
        <q-btn flat label="Anuluj" color="primary" v-close-popup />
        <q-btn
          flat
          label="Tak"
          color="negative"
          @click="deleteDocument()"
          v-close-popup
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>
<script>
import { ref, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useClientDocumentsStore } from "../../../stores/clientDocuments";
import { useQuasar } from "quasar";
import axios from "axios";

export default {
  props: {},
  setup(props) {
    const route = useRoute();
    const router = useRouter();
    const clientDocumentsStore = useClientDocumentsStore();
    const $q = useQuasar();
    let dialog = ref(false);
    onMounted(() => clientDocumentsStore.getClientDocuments(route.params.id));

    return {
      dialog,
      clientDocumentsStore,
      route,
      documentId: ref(null),
      deleteDocumentDialogVisible: ref(false),
      getUrl() {
        return "https://localhost:44370/api/ClientDocument/add";
      },
      onRejected(rejectedEntries) {
        $q.notify({
          type: 'negative',
          message: "Błąd przy próbie dodania dokumentów"
        });
      },
      onUploaded(info) {
        $q.notify({
          type: 'info',
          message: `Operacja zakończona pomyślnie`
        });
        dialog.value = false;
        clientDocumentsStore.getClientDocuments(route.params.id);
      },
      setDocumentToDelete(documentId) {
        this.deleteDocumentDialogVisible = true;
        this.documentId = documentId;
      },
      async deleteDocument() {
        await clientDocumentsStore.deleteDocument(this.documentId).then(
          () => {
            $q.notify({
              type: "info",
              message: `Dokument usunięty pomyślnie`,
            });
            clientDocumentsStore.getClientDocuments(route.params.id);
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
