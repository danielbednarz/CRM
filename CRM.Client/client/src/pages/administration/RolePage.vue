<template>
  <div class="row q-pa-lg">
    <div class="row q-pb-md"></div>
    <div class="col-md-12 col-xs-12">
      <q-table
        :rows="administrationStore.roles"
        :columns="columns"
        row-key="name"
        :pagination="initialPagination"
        title="Grupy"
        hide-bottom
      >
        <template v-slot:body="props">
          <q-tr :props="props" @click="moveToRoleDetails(props.row)">
            <q-td key="name" :props="props">
              {{ props.row.name }}
            </q-td>
          </q-tr>
        </template>
      </q-table>
    </div>
  </div>
</template>

<script>
import { defineComponent, onMounted } from "vue";
import { useAdministrationStore } from "../../stores/administration";
import { useRouter } from "vue-router";

export default {
  components: {},

  setup() {
    const administrationStore = useAdministrationStore();
    const router = useRouter();

    const columns = [
      {
        name: "name",
        label: "Nazwa",
        align: "left",
        field: "name",
        sortable: true,
        style: "font-size: 16px"
      },
    ];

    onMounted(() => administrationStore.getRoles());

    return {
      administrationStore,
      router,
      columns,
      initialPagination: {
        rowsPerPage: 10,
      },
      moveToRoleDetails(e) {
        router.push(`/administration/roles/${e.id}`);
      },
    };
  },
};
</script>
<style scoped>
.q-tr {
  cursor: pointer;
}
</style>
