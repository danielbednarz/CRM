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
      </q-toolbar>
    </q-header>

    <q-drawer
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
      <q-page class="q-pa-md">
        <router-view />
      </q-page>
    </q-page-container>
  </q-layout>
</template>

<script>
import { ref, computed } from "vue";
import { useRoute } from "vue-router";
import MenuLink from "../components/layout/MenuLink.vue";

export default {
  components: {
    MenuLink,
  },
  setup() {
    const route = useRoute();
    const leftDrawerOpen = ref(false);
    const miniState = ref(true);
    const currentRouteName = computed(() => route.name);
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
    ];

    return {
      leftDrawerOpen,
      miniState,
      toggleLeftDrawer() {
        leftDrawerOpen.value = !leftDrawerOpen.value;
      },
      menuList,
      currentRouteName,
    };
  },
};
</script>
<style scoped>
.link:active {
  color: black;
}
</style>
