import { boot } from "quasar/wrappers";
import axios from "axios";
import { Notify, Loading, Cookies, QSpinnerClock } from "quasar";
import { useAuthenticationStore } from "../stores/authentication";

// Be careful when using SSR for cross-request state pollution
// due to creating a Singleton instance here;
// If any client changes this (global) instance, it might be a
// good idea to move this instance creation inside of the
// "export default () => {}" function below (which runs individually
// for each client)
const api = axios.create({ baseURL: "https://localhost:44370/api" });

const authenticationStore = useAuthenticationStore();

api.interceptors.request.use(
  (config) => {
    Loading.show({
      spinner: QSpinnerClock
    });
    const token = Cookies.get("token");
    if (token) {
      config.headers["Authorization"] = "Bearer " + token;
    }
    else {
      authenticationStore.currentUser.username = "";
    }
    return config;
  },
  (error) => {
    Promise.reject(error);
  }
);

api.interceptors.response.use(
  (response) => {
    Loading.hide();
    return response;
  },
  (error) => {
    Loading.hide();
    Notify.create({
      type: "negative",
      message: `Błąd serwera: ${error.message}`,
    });
    return error;
  }
);

export default boot(({ app }) => {
  //appRoles
  app.config.globalProperties.$ADMIN = 'Administratorzy';
  app.config.globalProperties.$SUPERVISOR = 'Przełożeni';
  app.config.globalProperties.$PRIVILEGEDUSER = 'Uprzywilejowani użytkownicy';
  app.config.globalProperties.$USER = 'Użytkownicy';

  app.config.globalProperties.$axios = axios;
  app.config.globalProperties.$api = api;
});

export { api };
