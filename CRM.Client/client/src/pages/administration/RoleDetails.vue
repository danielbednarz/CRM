<template>
  <div class="q-pa-lg">
    <div class="q-gutter-y-md">
      <q-card v-if="administrationStore.role">
        <q-card-section>
          <div class="text-h4">
            {{ administrationStore.role.name }} ({{
              administrationStore.role.users.length
            }})
          </div>
        </q-card-section>
        <q-card-section>
          <div class="row q-pb-md">
            <div class="col-md-4 col-xs-10">
              <q-select
                flat
                v-model="usersToAdd"
                :options="administrationStore.usersAvailableToAdd"
                option-value="id"
                option-label="name"
                emit-value
                map-options
                use-chips
                stack-label
                multiple
                label="Użytkownicy"
                :disable="administrationStore.usersAvailableToAdd.length == 0"
                :hint="administrationStore.usersAvailableToAdd.length == 0 ? 'Brak użytkowników możliwych do dodania' : ''"
              />
            </div>
            <div class="col-md-2 col-xs-2">
              <q-btn
                flat
                class="q-mt-lg"
                :disable="usersToAdd.length == 0"
                color="primary"
                icon="fa-solid fa-plus"
                @click="addUsersToRole()"
              />
            </div>
          </div>
          <div class="row">
            <div class="col-md-12 col-xs-12">
              <q-table
                :rows="administrationStore.role.users"
                :columns="columns"
                row-key="name"
                :pagination="initialPagination"
              >
                <template v-slot:body="props">
                  <q-tr :props="props">
                    <q-td key="name" :props="props">
                      {{ props.row.name }}
                    </q-td>
                    <q-td key="action" :props="props">
                      <div class="q-gutter-sm">
                        <q-btn
                          flat
                          square
                          dense
                          :disable="administrationStore.role.users.length <= 1"
                          size="0.8em"
                          color="negative"
                          icon="fa-solid fa-trash"
                          @click="setDeleteUserFromRoleDialogData(props.row)"
                        />
                      </div>
                    </q-td>
                  </q-tr>
                </template>
              </q-table>
            </div>
          </div>
        </q-card-section>
      </q-card>
    </div>
  </div>
  <q-dialog v-model="deleteUserFromRoleDialogVisible">
    <q-card>
      <q-card-section class="row items-center">
        <q-icon
          name="fa-solid fa-triangle-exclamation"
          color="primary"
          size="lg"
        />
        <span class="q-ml-sm" style="font-size: 18px">
          Czy na pewno chcesz usunąć {{ userToDelete.name }} z grupy
          {{ administrationStore.role.name }}?
        </span>
      </q-card-section>

      <q-card-actions align="right">
        <q-btn flat label="Anuluj" color="primary" v-close-popup />
        <q-btn
          flat
          label="Tak"
          color="negative"
          @click="deleteUserFromRole()"
          v-close-popup
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>
<script>
import { useRoute, useRouter } from "vue-router";
import { onMounted, ref } from "vue";
import { useAdministrationStore } from "../../stores/administration";
import { useQuasar } from "quasar";

export default {
  setup() {
    const route = useRoute();
    const administrationStore = useAdministrationStore();
    const $q = useQuasar();

    const columns = [
      {
        name: "name",
        label: "Imię i nazwisko",
        align: "left",
        field: "name",
        sortable: true,
        style: "font-size: 16px",
      },
      {
        name: "action",
        label: "Akcja",
        align: "left",
        field: "action",
        sortable: false,
        style: "width: 10%;",
      },
    ];

    onMounted(() => administrationStore.getRoleDetails(route.params.id));
    onMounted(() =>
      administrationStore.getUsersAvailableToAdd(route.params.id)
    );

    return {
      route,
      administrationStore,
      columns,
      deleteUserFromRoleDialogVisible: ref(false),
      usersToAdd: ref([]),
      userToDelete: ref({
        id: null,
        name: "",
      }),
      initialPagination: {
        rowsPerPage: 15,
      },
      setDeleteUserFromRoleDialogData(user) {
        this.userToDelete.id = user.id;
        this.userToDelete.name = user.name;
        this.deleteUserFromRoleDialogVisible = true;
      },
      async addUsersToRole() {
        await administrationStore
          .addUsersToRole({ roleId: route.params.id, userIds: this.usersToAdd })
          .then((response) => {
            if (response.status == 200) {
              $q.notify({
                type: "info",
                message: "Użytkownicy zostali dodani do grupy",
              });
              this.usersToAdd = [];
              administrationStore.getRoleDetails(route.params.id);
              administrationStore.getUsersAvailableToAdd(route.params.id)
            } else {
              $q.notify({
                type: "negative",
                message: "Błąd przy próbie dodania użytkowników",
              });
            }
          });
      },
      async deleteUserFromRole() {
        await administrationStore
          .deleteUserFromRole(this.userToDelete.id, route.params.id)
          .then((response) => {
            if (response.status == 200) {
              $q.notify({
                type: "info",
                message: "Użytkownik został usunięty z grupy",
              });
              administrationStore.getRoleDetails(route.params.id);
              administrationStore.getUsersAvailableToAdd(route.params.id)
            } else {
              $q.notify({
                type: "negative",
                message: "Błąd przy próbie usunięcia",
              });
            }
          });
      },
    };
  },
};
</script>
