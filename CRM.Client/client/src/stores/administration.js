import { defineStore } from "pinia";
import { api } from "src/boot/axios";

export const useAdministrationStore = defineStore("administration", {
  state: () => ({
    users: []
  }),
  actions: {
    getUsers() {
      api.get("/Administration/getUsers").then((response) => {
        this.users = response.data;
      });
    }
  },
});
