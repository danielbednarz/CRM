<template>
<div v-if="tasksStore.task">
  <task-details-data :task="tasksStore.task" />
</div>
</template>
<script>
import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";
import { useTasksStore } from "../../stores/tasks";
import TaskDetailsData from "../../components/tasks/taskDetails/TaskDetailsData.vue";
export default {
  components: {
    TaskDetailsData
  },
  setup() {
    const tasksStore = useTasksStore();
    const route = useRoute();
    onMounted(() => tasksStore.getTaskDetails(route.params.id));

    return {
      tasksStore,
      route,
    };
  },
};
</script>
