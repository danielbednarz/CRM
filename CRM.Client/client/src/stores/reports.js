import { defineStore } from "pinia";
import { api } from "src/boot/axios";

export const useReportsStore = defineStore("reports", {
  state: () => ({
  }),
  actions: {
    async getNewClientsCount() {
      return await api
        .get("/Reports/getNewClientsCount");
    },
    async getNewTasksCount() {
      return await api
        .get("/Reports/getNewTasksCount");
    },
  },
});
