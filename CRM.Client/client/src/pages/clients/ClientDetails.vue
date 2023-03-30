<template>
  <div class="q-pa-lg">
    <div class="q-gutter-y-md">
      <q-card>
        <q-tabs
          v-model="tab"
          dense
          class="text-grey"
          active-color="primary"
          indicator-color="primary"
          align="justify"
          narrow-indicator
        >
          <q-tab name="client" label="Dane klienta" />
          <q-tab name="notes" label="Wydarzenia" />
          <q-tab name="documents" label="Dokumenty" />
        </q-tabs>

        <q-separator />

        <q-tab-panels v-model="tab" animated>
          <q-tab-panel name="client">
            <div class="text-h6" v-if="clientsStore.client">
              <client-details-data :client="clientsStore.client" />
            </div>
          </q-tab-panel>

          <q-tab-panel name="notes">
              <client-notes />
          </q-tab-panel>

          <q-tab-panel name="documents">
            <client-documents />
          </q-tab-panel>
          </q-tab-panels>
      </q-card>
    </div>
  </div>
</template>
<script>
import { ref, onMounted, watch } from "vue";
import { useRoute } from "vue-router";
import { useClientsStore } from "../../stores/clients";
import ClientDetailsData from "../../components/clients/clientDetails/ClientDetailsData.vue";
import ClientNotes from '../../components/clients/clientDetails/ClientNotes.vue'
import ClientDocuments from '../../components/clients/clientDetails/ClientDocuments.vue'

export default {
  components: {
    ClientDetailsData,
    ClientNotes,
    ClientDocuments
  },
  setup() {
    const clientsStore = useClientsStore();
    const route = useRoute();
    onMounted(() => clientsStore.getClientById(route.params.id));

    return {
      tab: ref("client"),
      clientsStore,
      route,
    };
  },
};
</script>
