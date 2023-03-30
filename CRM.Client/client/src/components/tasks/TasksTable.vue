<template>
  <div class="col-12">
    <q-grid
      :data="tasksStore.tasks"
      :title="getTasksCount()"
      :columns="columns"
      :columns_filter="true"
      :draggable="true"
      :fullscreen="false"
      :csv_download="true"
      :global_search="false"
    >
      <template v-slot:body="props">
        <q-tr :props="props" @click="moveToTaskDetails(props.row)">
          <q-td key="signature">
            <b>{{ props.row.signature }}</b>
          </q-td>
          <q-td key="clientName">
            {{ props.row.clientName }}
          </q-td>
          <q-td key="assignedUser" class="">
            {{ props.row.assignedUser }}
          </q-td>
          <q-td key="supervisor">
            {{ props.row.supervisor }}
          </q-td>
          <q-td key="createdDate">
            {{ new Date(props.row.createdDate).toLocaleDateString("pl-PL") }}
          </q-td>
          <q-td key="completionDate">
            {{ new Date(props.row.completionDate).toLocaleDateString("pl-PL") }}
          </q-td>
          <q-td key="step">
            {{ props.row.step }}
          </q-td>
          <q-td key="priority">
              {{ props.row.priority }}
          </q-td>
        </q-tr>
      </template>
    </q-grid>
  </div>
</template>
<script>
import { useTasksStore } from "../../stores/tasks";
import { exportFile } from "quasar";
import { onMounted } from "vue";
import { useRouter } from "vue-router";

const columns = [
  {
    name: "signature",
    label: "Sygnatura",
    align: "left",
    field: "signature",
    sortable: true,
  },
  {
    name: "clientName",
    label: "Klient",
    align: "left",
    field: "clientName",
    sortable: true,
  },
  {
    name: "assignedUser",
    label: "Osoba przypisana",
    align: "left",
    field: "assignedUser",
    sortable: false,
    filter_type: "select",
  },
  {
    name: "supervisor",
    label: "Osoba nadzorująca",
    align: "left",
    field: "supervisor",
    sortable: false,
    filter_type: "select",
  },
  {
    name: "createdDate",
    label: "Data utworzenia",
    align: "left",
    field: "createdDate",
    sortable: true,
    filter_type: "date",
  },
  {
    name: "completionDate",
    label: "Termin zakończenia",
    align: "left",
    field: "completionDate",
    sortable: true,
  },
  {
    name: "step",
    label: "Krok",
    align: "left",
    field: "step",
    sortable: true,
    filter_type: "select",
  },
  {
    name: "priority",
    label: "Priorytet",
    align: "left",
    field: "priority",
    sortable: true,
    filter_type: "select",
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
    const tasksStore = useTasksStore();
    onMounted(() => tasksStore.getAllTasks());

    return {
      tasksStore,
      columns,
      exportTable() {
        const content = [columns.map((col) => wrapCsvValue(col.label))]
          .concat(
            tasksStore.clients.map((row) =>
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

        const status = exportFile("tasks.csv", content, "text/csv");

        if (status !== true) {
          $q.notify({
            message: "Browser denied file download...",
            color: "negative",
            icon: "warning",
          });
        }
      },
      initialPagination: {
        rowsPerPage: 10,
      },
      getTasksCount() {
        let c = tasksStore.tasks.length;
        return `Zadania (${c})`;
      },
      moveToTaskDetails(e) {
        router.push(`/tasks/${e.id}`);
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
