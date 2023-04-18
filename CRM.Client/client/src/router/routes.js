import { Cookies } from "quasar";

const routes = [
  {
    path: "/confirmEmail",
    name: "ConfirmEmail",
    component: () => import("pages/ConfirmEmail.vue"),
    props: route => ({
      email: route.query.email,
      token: route.query.token
    })
  },
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
      {
        path: "/tasks/add",
        name: "taskAdd",
        beforeEnter: (to, from, next) => authGuard(to, from, next),
        component: () => import("pages/tasks/TaskAdd.vue"),
      },
      {
        path: "/tasks/:id",
        name: "taskDetails",
        beforeEnter: (to, from, next) => authGuard(to, from, next),
        component: () => import("pages/tasks/TaskDetails.vue"),
      },
      {
        path: "/administration",
        name: "administration",
        beforeEnter: (to, from, next) => authGuard(to, from, next),
        component: () => import("pages/administration/AdministrationPage.vue"),
      },
      {
        path: "/administration/users",
        name: "users",
        beforeEnter: (to, from, next) => authGuard(to, from, next),
        component: () => import("pages/administration/UsersPage.vue"),
      },
      {
        path: "/administration/users/:id",
        name: "userDetails",
        beforeEnter: (to, from, next) => authGuard(to, from, next),
        component: () => import("pages/administration/UserDetails.vue"),
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
