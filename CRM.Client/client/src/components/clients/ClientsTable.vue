<template>
  <div class="col-12">
    <q-table
      title="Klienci"
      :rows="clientsStore.clients"
      :columns="columns"
      row-key="name"
    >
      <template v-slot:body="props">
        <q-tr :props="props" @click="moveToClientDetails(props.row)">
          <q-td key="name" :props="props">
            {{ props.row.name }}
          </q-td>
          <q-td key="nip" :props="props">
            {{ props.row.nip }}
          </q-td>
          <q-td key="country" :props="props">
            {{ props.row.country }}
          </q-td>
          <q-td key="city" :props="props">
            {{ props.row.city }}
          </q-td>
          <q-td key="street" :props="props">
            {{ props.row.street }}
          </q-td>
          <q-td key="isActive" :props="props">
            <q-icon
              class="q-pl-md"
              v-if="props.row.isActive"
              name="fa-solid fa-square-check"
              color="green"
              size="sm"
            >
            </q-icon>
            <q-icon
              class="q-pl-md"
              v-if="!props.row.isActive"
              name="fa-solid fa-square-xmark"
              color="red"
              size="sm"
            >
            </q-icon>
          </q-td>
          <q-td key="action">
            <div class="q-gutter-sm">
              <q-btn
                flat
                square
                dense
                size="0.9em"
                color="primary"
                icon="fa-solid fa-info"
                @click="moveToClientDetails(props.row)"
              />
            </div>
          </q-td>
        </q-tr>
      </template>
      <template v-slot:top-right>
        <q-btn
          color="primary"
          icon-right="archive"
          label="Eksportuj do csv"
          no-caps
          @click="exportTable"
        />
      </template>
    </q-table>
  </div>
</template>
<script>
import { useClientsStore } from "../../stores/clients";
import { exportFile } from "quasar";
import { onMounted } from "vue";
import { useRouter } from "vue-router";

const columns = [
  {
    name: "name",
    label: "Nazwa",
    align: "left",
    field: "name",
    sortable: true,
  },
  {
    name: "nip",
    label: "Nip",
    align: "left",
    field: "nip",
    sortable: false,
  },
  {
    name: "country",
    label: "Kraj",
    align: "left",
    field: "country",
    sortable: false,
  },
  {
    name: "city",
    label: "Miasto",
    align: "left",
    field: "city",
    sortable: false,
  },
  {
    name: "street",
    label: "Ulica",
    align: "left",
    field: "street",
    sortable: false,
  },
  {
    name: "isActive",
    label: "Czy aktywny",
    align: "left",
    field: "isActive",
    sortable: false,
  },
  {
    name: "action",
    align: "left",
    label: "Akcja",
    field: "action",
    sortable: true,
  },
];

function wrapCsvValue(val, formatFn, row) {
  let formatted = formatFn !== void 0 ? formatFn(val, row) : val;

  formatted =
    formatted === void 0 || formatted === null ? "" : String(formatted);

  formatted = formatted.split('"').join('""');

  return `"${formatted}"`;
}

export default {
  setup() {
    const router = useRouter();
    const clientsStore = useClientsStore();
    onMounted(() => clientsStore.getClients());

    return {
      clientsStore,
      columns,
      exportTable() {
        const content = [columns.map((col) => wrapCsvValue(col.label))]
          .concat(
            clientsStore.clients.map((row) =>
              columns
                .map((col) =>
                  wrapCsvValue(
                    typeof col.field === "function"
                      ? col.field(row)
                      : row[col.field === void 0 ? col.name : col.field],
                    col.format,
                    row
                  )
                )
                .join(",")
            )
          )
          .join("\r\n");

        const status = exportFile("clients.csv", content, "text/csv");

        if (status !== true) {
          $q.notify({
            message: "Browser denied file download...",
            color: "negative",
            icon: "warning",
          });
        }
      },
      moveToClientDetails(e) {
        router.push(`/clients/${e.id}`);
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
