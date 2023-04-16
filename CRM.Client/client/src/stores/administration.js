import { defineStore } from "pinia";
import { api } from "src/boot/axios";

export const useAdministrationStore = defineStore("administration", {
  state: () => ({
    users: [],
    user: null
  }),
  actions: {
    getUsers() {
      api.get("/Administration/getUsers").then((response) => {
        this.users = response.data;
      });
    },
    getUserDetails(id) {
      api.get("/Administration/getUserDetails", { params: { id: id } }).then((response) => {
        this.user = response.data;
      });
    },
    async editUser() {
      await api.put("Administration/editUser", {
        Id: this.user.id,
        FirstName: this.user.firstName,
        LastName: this.user.lastName,
        Email: this.user.email,
        PhoneNumber: this.user.phoneNumber,
        IsActive: this.user.isActive,
      });
    },
  },
});
