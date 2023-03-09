<template>
  <div class="row q-pa-lg">
    <div class="row q-pb-md">
      <q-btn
        outline
        label="Dodaj klienta"
        color="primary"
        icon-right="fa-solid fa-address-card"
        @click="moveToClientAdd()"
      />
      <q-btn
        outline
        class="q-ml-md"
        label="Importuj klientów"
        color="primary"
        icon-right="fa-solid fa-file-import"
        @click="dialog = true"
      />
    </div>
    <clients-table></clients-table>
    <q-dialog v-model="dialog">
      <q-card>
          <q-uploader
            :url="getUrl"
            label="Importuj klientów"
            :headers="[
              {
                name: 'Authorization',
                value: `Bearer ${token}`,
              },
            ]"
            :filter="checkFileType"
            @rejected="onRejected"
            @uploaded="onUploaded"
            max-files="1"
            style="width: 320px; height:150px"
          />
          <q-card-section>Pamiętaj, plik musi mieć rozszerzenie xlsx.</q-card-section>
      </q-card>
    </q-dialog>
  </div>
</template>
<script>
import ClientsTable from "../../components/clients/ClientsTable.vue";
import { useClientsStore } from "../../stores/clients";
import { useRouter } from "vue-router";
import { ref } from "vue";
import { api } from "src/boot/axios";
import { Cookies, useQuasar } from "quasar";

export default {
  components: {
    ClientsTable,
  },
  setup() {
    const router = useRouter();
    let dialog = ref(false);
    const token = Cookies.get("token");
    const $q = useQuasar();
    const clientsStore = useClientsStore();

    return {
      router,
      dialog,
      api,
      token,
      getUrl(files) {
        return "https://localhost:44370/api/Clients/importClientsFromFile";
      },
      moveToClientAdd() {
        router.push("clients/add");
      },
      checkFileType(files) {
        return files.filter(file => file.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet')
      },
      onRejected(rejectedEntries) {
        $q.notify({
          type: 'negative',
          message: "Plik musi być typu .xlsx"
        });
      },
      onUploaded(info) {
        $q.notify({
          type: 'info',
          message: `Liczba zaimportowanych klientów: ${info.xhr.responseText}`
        });
        dialog.value = false;
        clientsStore.getClients();
      }
    };
  },
};
</script>
