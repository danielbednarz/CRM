import { defineStore } from "pinia";
import { api } from "src/boot/axios";

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
    async editClient() {
      await api.put("Clients/editClient", {
        Id: this.client.id,
        Name: this.client.name,
        Nip: this.client.nip,
        Krs: this.client.krs,
        Regon: this.client.regon,
        Rating: this.client.rating,
        Country: this.client.country,
        City: this.client.city,
        Street: this.client.street,
        BuildingNumber: this.client.buildingNumber,
        IsActive: this.client.isActive,
      });
    },
    async deleteClient() {
      debugger;
      await api.delete("Clients/deleteClient", {
        params: {
          id: this.client.id,
        },
      });
    },
    async addClient(clientToAdd) {
      await api.post("Clients/addClient", clientToAdd);
    },
  },
});
