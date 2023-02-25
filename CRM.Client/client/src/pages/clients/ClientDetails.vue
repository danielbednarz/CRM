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
          <q-tab name="alarrms" label="Dane kontakowe" />
          <q-tab name="movies" label="Dokumenty" />
        </q-tabs>

        <q-separator />

        <q-tab-panels v-model="tab" animated>
          <q-tab-panel name="client">
            <div class="text-h6" v-if="clientsStore.client">
              <client-details-data :client="clientsStore.client" />
            </div>
          </q-tab-panel>

          <q-tab-panel name="alarrms">
            <div class="text-h6">Alarms</div>
            Lorem ipsum dolor sit amet consectetur adipisicing elit.
          </q-tab-panel>

          <q-tab-panel name="movies">
            <div class="text-h6">Movies</div>
            Lorem ipsum dolor sit amet consectetur adipisicing elit.
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

export default {
  components: {
    ClientDetailsData,
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
