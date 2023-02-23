import { defineStore } from "pinia";
import { api } from "src/boot/axios";
import { Cookies } from "quasar";

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
  getters: {
    getToken: (state) => state.currentUser.token,
  },
  actions: {
    async login() {
      await api.post("/Account/login", this.form).then((response) => {
        this.currentUser.username = response.data.username;
        this.currentUser.token = response.data.token;
        Cookies.set("token", response.data.token, {
          expires: "3h",
        });
        Cookies.set("username", response.data.username);
        this.router.push("/");
      });
    },
    async register() {
      await api.post("/Account/register", this.form).then((response) => {
        this.currentUser.username = response.data.username;
        this.currentUser.token = response.data.token;
        Cookies.set("token", response.data.token, {
          expires: "3h",
        });
        Cookies.set("username", response.data.username);
      });
    },
    logout() {
      this.currentUser.username = "";
      this.currentUser.token = "";
      Cookies.remove("username");
      Cookies.remove("token");
    },
    clearForm() {
      this.form.Username = "";
      this.form.Password = "";
    },
  },
});
