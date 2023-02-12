const routes = [
  {
    path: "/",
    component: () => import("layouts/MainLayout.vue"),
    children: [
      {
        path: "",
        name: "home",
        beforeEnter: (to, from, next) => authGuard(to, from, next),
        component: () => import("pages/HomePage.vue"),
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
  if (localStorage.getItem("user")) {
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
