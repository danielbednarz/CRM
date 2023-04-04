<template>
  <div class="row q-pa-lg">
    <div class="row q-pb-md">
      <q-btn
        outline
        label="Nowe zadanie"
        color="primary"
        icon-right="fa-solid fa-pen-to-square"
        @click="moveToTasksAdd()"
      />
    </div>
<tasks-table />
  </div>
</template>
<script>
import TasksTable from "../../components/tasks/TasksTable.vue";
import { useRouter } from "vue-router";

export default {
  components: {
    TasksTable
  },
  setup() {
    const router = useRouter();

    return {
      router,
      moveToTasksAdd() {
        router.push("tasks/add");
      }
    }
  }
}
</script>
<style scoped>
.q-tr {
  cursor: pointer;
}
</style>
