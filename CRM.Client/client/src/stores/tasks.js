import { defineStore } from "pinia";
import { api } from "src/boot/axios";

export const useTasksStore = defineStore("tasks", {
  state: () => ({
    tasks: [],
  }),
  actions: {
    getAllTasks() {
      api
        .get("/Tasks/getAllTasks")
        .then((response) => {
          this.tasks = response.data;
        });
    },
  },
});
