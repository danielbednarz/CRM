import { defineStore } from "pinia";
import { api } from "src/boot/axios";

export const useAdministrationStore = defineStore("administration", {
  state: () => ({
    users: [],
    user: null,
    roles: [],
    role: null,
    usersAvailableToAdd: [],
  }),
  actions: {
    getUsers() {
      api.get("/Administration/getUsers").then((response) => {
        this.users = response.data;
      });
    },
    getUserDetails(id) {
      api
        .get("/Administration/getUserDetails", { params: { id: id } })
        .then((response) => {
          this.user = response.data;
        });
    },
    getUsersAvailableToAdd(roleId) {
      api
        .get("/Administration/getUsersAvailableToAdd", {
          params: { roleId: roleId },
        })
        .then((response) => {
          this.usersAvailableToAdd = response.data;
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
    getRoles() {
      api.get("/Administration/getRoles").then((response) => {
        this.roles = response.data;
      });
    },
    getRoleDetails(id) {
      api
        .get("/Administration/getRoleDetails", { params: { id: id } })
        .then((response) => {
          this.role = response.data;
        });
    },
    async addUsersToRole(usersToAdd) {
      return await api.post("Administration/addUsersToRole", usersToAdd);
    },
    async deleteUserFromRole(userId, roleId) {
      return await api.delete("Administration/deleteUserFromRole", {
        params: {
          userId: userId,
          roleId: roleId,
        },
      });
    },
  },
});
