import { defineStore } from "pinia";
import { api } from "src/boot/axios";
import { useAuthenticationStore } from "./authentication";

const authenticationStore = useAuthenticationStore();

export const useClientsStore = defineStore("clients", {
  state: () => ({
    clients: [],
    client: null,
  }),
  actions: {
    getClients() {
      api.get("/Clients/getClients").then((response) => {
        this.clients = response.data;
      });
    },
    getClientById(id) {
      api
        .get("/Clients/getClientById", { params: { id: id } })
        .then((response) => {
          this.client = response.data;
        });
    },
  },
});
