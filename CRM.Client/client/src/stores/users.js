import { defineStore } from "pinia";
import { api } from "src/boot/axios";

export const useUsersStore = defineStore("users", {
  state: () => ({
    users: []
  }),
  actions: {
    getUsers() {
      api.get("/Users/getUsers").then((response) => {
        this.users = response.data;
      });
    }
  },
});
