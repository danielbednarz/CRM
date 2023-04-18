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
      roles: [],
      token: "",
    },
  }),
  getters: {
    getToken: (state) => state.currentUser.token,
  },
  actions: {
    async login() {
      await api.post("/Account/login", this.form).then((response) => {
        this.setCurrentUser(response.data);
        this.router.push("/");
        this.clearForm();
      });
    },
    async register(userToAdd) {
      debugger;
      return await api.post("/Account/register", userToAdd);
    },
    async confirmEmail(userId, token) {
      return await api.get("Account/confirmEmail", { params: {
        id: userId,
        token: token
      }});
    },
    logout() {
      this.currentUser.username = "";
      this.currentUser.token = "";
      Cookies.remove("username");
      Cookies.remove("token");
    },
    clearForm() {
      this.form.username = "";
      this.form.password = "";
    },
    setCurrentUser(user) {
      const roles = this.getDecodedToken(user.token).role;
        this.currentUser.username = user.username;
        Array.isArray(roles) ? this.currentUser.roles = roles : this.currentUser.roles.push(roles);
        this.currentUser.token = user.token;
        Cookies.set("token", user.token, {
          expires: "3h",
        });
        Cookies.set("username", user.username);
    },
    getDecodedToken(token) {
      return JSON.parse(atob(token.split('.')[1]));
    }
  },
  persist: true
});
