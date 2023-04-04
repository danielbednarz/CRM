import { defineStore } from "pinia";
import { api } from "src/boot/axios";

export const useTasksStore = defineStore("tasks", {
  state: () => ({
    tasks: [],
    task: null
  }),
  actions: {
    getAllTasks() {
      api
        .get("/Tasks/getAllTasks")
        .then((response) => {
          this.tasks = response.data;
        });
    },
    getTaskDetails(taskId) {
      api
        .get("/Tasks/getTaskDetails", { params: { taskId: taskId } })
        .then((response) => {
          this.task = response.data;
        });
    },
    async addTask(model) {
      return api.post("Tasks/addTask", model);
    },
    async moveToNextStep(taskId) {
      await api.post("Tasks/moveToNextStep", null, {
        params: {
          taskId: taskId
        }
      });
    },
    async moveToPreviousStep(taskId) {
      await api.post("Tasks/moveToPreviousStep", null, {
        params: {
          taskId: taskId
        }
      });
    },
    async cancelTask(taskId) {
      await api.post("Tasks/cancelTask", null, {
        params: {
          taskId: taskId
        }
      });
    }
  },
});
