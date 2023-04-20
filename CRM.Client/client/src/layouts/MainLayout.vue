<template>
  <q-layout view="hHh lpR fFf">
    <q-header elevated class="bg-primary text-white" height-hint="98">
      <q-toolbar>
        <q-btn dense flat round icon="menu" @click="toggleLeftDrawer" />

        <q-toolbar-title>
          <q-avatar>
            <img src="https://cdn.quasar.dev/logo-v2/svg/logo-mono-white.svg" />
          </q-avatar>
          CRM
        </q-toolbar-title>
          <div class="text-h6 q-pr-md">{{currentUserName}}</div>
          <q-btn flat icon="fa-solid fa-right-from-bracket" @click="logout()" />
      </q-toolbar>
    </q-header>

    <q-drawer
      class="bg-grey-3"
      show-if-above
      v-model="leftDrawerOpen"
      side="left"
      bordered
      :mini="miniState"
      @mouseover="miniState = false"
      @mouseout="miniState = true"
      mini-to-overlay
    >
      <q-list>
        <template v-for="(menuItem, index) in menuList" :key="index">
          <menu-link
            :name="menuItem.name"
            :currentRouteName="currentRouteName"
            :icon="menuItem.icon"
            :label="menuItem.label"
            :to="menuItem.to"
          />
          <q-separator :key="'sep' + index" v-if="menuItem.separator" />
        </template>
      </q-list>
    </q-drawer>

    <q-page-container>
      <q-page class="q-pa-md bg-color">
        <router-view />
      </q-page>
    </q-page-container>
  </q-layout>
</template>

<script>
import { ref, computed } from "vue";
import { useRoute, useRouter } from "vue-router";
import { Cookies, useQuasar } from "quasar";
import { useAuthenticationStore } from "../stores/authentication";
import MenuLink from "../components/layout/MenuLink.vue";

export default {
  components: {
    MenuLink,
  },
  setup() {
    const route = useRoute();
    const router = useRouter();
    const $q = useQuasar();
    const authenticationStore = useAuthenticationStore();
    const leftDrawerOpen = ref(false);
    const miniState = ref(true);
    const currentRouteName = computed(() => route.name);
    const currentUserName = computed(() => authenticationStore.currentUser.username);
    const menuList = [
      {
        icon: "fa-solid fa-house",
        label: "Panel główny",
        separator: true,
        name: "home",
        to: "/home",
      },
      {
        icon: "fa-solid fa-users",
        label: "Klienci",
        separator: false,
        name: "clients",
        to: "/clients",
      },
      {
        icon: "fa-solid fa-list-check",
        label: "Zadania",
        separator: false,
        name: "tasks",
        to: "/tasks",
      },
      {
        icon: "fa-solid fa-gears",
        label: "Administracja",
        separator: false,
        name: "administration",
        to: "/administration",
      },
    ];

    return {
      leftDrawerOpen,
      miniState,
      menuList,
      currentRouteName,
      currentUserName,
      toggleLeftDrawer() {
        leftDrawerOpen.value = !leftDrawerOpen.value;
      },
      logout() {
        authenticationStore.logout();
        $q.notify({
          type: "info",
          message: `Wylogowano pomyślnie`,
        });
        router.push("/login");
      }
    };
  },
};
</script>
<style scoped>
.link:active {
  color: black;
}
</style>
