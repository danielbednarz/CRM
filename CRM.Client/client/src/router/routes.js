import { Cookies } from "quasar";

const routes = [
  {
    path: "/",
    component: () => import("layouts/MainLayout.vue"),
    children: [
      {
        path: "/home",
        name: "home",
        beforeEnter: (to, from, next) => authGuard(to, from, next),
        component: () => import("pages/HomePage.vue"),
        alias: ["/"],
      },
      {
        path: "/login",
        name: "login",
        component: () => import("pages/LoginPage.vue"),
      },
      {
        path: "/clients",
        name: "clients",
        beforeEnter: (to, from, next) => authGuard(to, from, next),
        component: () => import("pages/clients/ClientsPage.vue"),
      },
      {
        path: "/clients/add",
        name: "clientAdd",
        beforeEnter: (to, from, next) => authGuard(to, from, next),
        component: () => import("pages/clients/ClientAdd.vue"),
      },
      {
        path: "/clients/:id",
        name: "clientDetails",
        beforeEnter: (to, from, next) => authGuard(to, from, next),
        component: () => import("pages/clients/ClientDetails.vue"),
      },
      {
        path: "/clients/edit/:id",
        name: "clientEdit",
        beforeEnter: (to, from, next) => authGuard(to, from, next),
        component: () => import("pages/clients/ClientEdit.vue"),
      },
      {
        path: "/tasks",
        name: "tasks",
        beforeEnter: (to, from, next) => authGuard(to, from, next),
        component: () => import("pages/tasks/TasksPage.vue"),
      },
    ],
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: "/:catchAll(.*)*",
    component: () => import("pages/ErrorNotFound.vue"),
  },
];

function authGuard(to, from, next) {
  let isAuthenticated = false;
  if (Cookies.has("token")) {
    isAuthenticated = true;
  } else {
    isAuthenticated = false;
  }
  if (isAuthenticated) {
    next();
  } else {
    next({ name: "login" });
  }
}

export default routes;
