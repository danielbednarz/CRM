import { defineStore } from "pinia";
import { api } from "src/boot/axios";

export const useAuthenticationStore = defineStore("authentication", {
  state: () => ({
    form: {
      email: "",
      password: "",
    },
    currentUser: {
      username: "",
      token: "",
    },
  }),
  actions: {
    async login() {
      await api.post("/Account/login", this.form).then((response) => {
        this.currentUser.username = response.data.username;
        this.currentUser.token = response.data.token;
        localStorage.setItem("user", JSON.stringify(response.data));
        this.router.push("/");
      });
    },
    async register() {
      await api.post("/Account/register", this.form).then((response) => {
        this.currentUser.username = response.data.username;
        this.currentUser.token = response.data.token;
        localStorage.setItem("user", JSON.stringify(response.data));
      });
    },
    logout() {
      this.currentUser.username = "";
      this.currentUser.token = "";
      this.signalrConnection.stop();
      localStorage.removeItem("user");
      this.signalrConnection = null;
    },
    clearForm() {
      this.form.Username = "";
      this.form.Password = "";
    },
  },
});
