<template>
  <div class="row q-pa-lg">
    <div class="row q-pb-md">
      <q-btn
        outline
        label="Nowe zadanie"
        color="primary"
        icon-right="fa-solid fa-pen-to-square"
      />
    </div>
<tasks-table />
  </div>
</template>
<script>
import TasksTable from "../../components/tasks/TasksTable.vue";

export default {
  components: {
    TasksTable
  },
  setup() {
    return {

    }
  }
}
</script>
<style scoped>
.q-tr {
  cursor: pointer;
}
</style>
