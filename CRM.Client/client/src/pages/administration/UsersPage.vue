<template>
  <div class="row q-pa-lg">
    <div class="row q-pb-md">
      <q-btn
        outline
        label="Zarejestruj nowego użytkownika"
        color="primary"
        icon-right="fa-solid fa-address-card"
        :to="{ name: 'userAdd' }"
        class="bg-white"
      />
    </div>
    <div class="col-md-12 col-xs-12">
      <q-table
        :title="getUsersCount()"
        :rows="administrationStore.users"
        :columns="columns"
        row-key="name"
        :pagination="initialPagination"
      >
        <template v-slot:body="props">
          <q-tr :props="props" @click="moveToUserDetails(props.row)">
            <q-td key="firstName" :props="props">
              {{ props.row.firstName }}
            </q-td>
            <q-td key="lastName" :props="props">
              {{ props.row.lastName }}
            </q-td>
            <q-td key="email" :props="props">
              {{ props.row.email }}
            </q-td>
            <q-td key="phoneNumber" :props="props">
              {{ props.row.phoneNumber }}
            </q-td>
            <q-td key="isActive" :props="props">
              <q-icon
                class="q-pl-md"
                :name="props.row.isActive ? 'fa-solid fa-square-check' : 'fa-solid fa-square-xmark'"
                :color="props.row.isActive ? 'green' : 'red'"
                size="sm"
            />
            </q-td>
            <q-td key="roles" :props="props">
              {{ props.row.roles.map(a => a.name) }}
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
        name: "firstName",
        label: "Imię",
        align: "left",
        field: "firstName",
        sortable: true,
      },
      {
        name: "lastName",
        label: "Nazwisko",
        align: "left",
        field: "lastName",
        sortable: true,
      },
      {
        name: "email",
        label: "Email",
        align: "left",
        field: "email",
        sortable: true,
      },
      {
        name: "phoneNumber",
        label: "Numer telefonu",
        align: "left",
        field: "phoneNumber",
        sortable: false,
      },
      {
        name: "isActive",
        label: "Czy aktywny?",
        align: "left",
        field: "isActive",
        sortable: true,
      },
      {
        name: "roles",
        label: "Grupy",
        align: "left",
        field: "roles",
        sortable: false,
      },
    ];

    onMounted(() => administrationStore.getUsers());

    return {
      administrationStore,
      router,
      columns,
      getUsersCount() {
        let c = administrationStore.users.length;
        return `Użytkownicy (${c})`;
      },
      initialPagination: {
        rowsPerPage: 10
      },
      moveToUserDetails(e) {
        router.push(`/administration/users/${e.id}`);
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
