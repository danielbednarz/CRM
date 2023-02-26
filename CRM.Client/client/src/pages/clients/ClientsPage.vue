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
    </div>
    <clients-table></clients-table>
  </div>
</template>
<script>
import ClientsTable from "../../components/clients/ClientsTable.vue";
import { useRouter } from "vue-router";

export default {
  components: {
    ClientsTable,
  },
  setup() {
    const router = useRouter();

    return {
      router,
      moveToClientAdd() {
        router.push("clients/add");
      },
    };
  },
};
</script>
