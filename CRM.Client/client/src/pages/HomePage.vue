<template>
  <div class="q-pa-lg">
    <q-card class="row q-pa-md shadow-4">
      <div class="col-md-2 col-xs-12">
        <h2 style="margin: 0;">Twój panel</h2>
      </div>
      <div class="col-md-8">
        <q-btn round icon="notifications">
          <q-badge floating color="red" rounded :label="tasksStore.tasksCount"/>
        </q-btn>
      </div>
      <div class="col-md-2 col-xs-12">
        <clock class="q-pl-xl"/>
      </div>

    </q-card>
  </div>
  <div class="row q-pa-lg">
    <menu-tile route="/clients" class="q-pr-sm" title="Klienci" icon="fa-solid fa-users" color="bg-accent" />
    <menu-tile route="/tasks" class="q-px-sm" title="Zadania" icon="fa-solid fa-list-check" color="bg-accent" />
    <menu-tile route="/reports" class="q-px-sm" title="Raporty" icon="fa-solid fa-chart-line" color="bg-accent" />
    <menu-tile route="/administration" class="q-pl-sm" title="Administracja" icon="fa-solid fa-gears" color="bg-accent" v-if="currentUser.roles.includes($ADMIN)"/>
  </div>
</template>

<script>
import { defineComponent, onMounted, ref } from "vue";
import MenuTile from "../components/layout/MenuTile.vue";
import Clock from "../components/Clock.vue";
import { useTasksStore } from "../stores/tasks";
import { useAuthenticationStore } from "../stores/authentication";

export default {
  components: {
    MenuTile,
    Clock
  },
  setup() {
    const tasksStore = useTasksStore();
    const currentUser = useAuthenticationStore().currentUser;
    onMounted(() => {
      tasksStore.getUserTasksCount();
    });

    return {
      tasksStore,
      currentUser
    }
  },
};
</script>

<style scoped>
.box {
  background: rgb(255, 255, 255);
  cursor: pointer;
  background-color: #fff;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
  -webkit-transition: all 0.6s cubic-bezier(0.165, 0.84, 0.44, 1);
  transition: all 0.6s cubic-bezier(0.165, 0.84, 0.44, 1);
}

.box::after {
  content: "";
  border-radius: 5px;
  position: absolute;
  z-index: -1;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  box-shadow: 0 5px 15px rgba(0, 0, 0, 0.3);
  opacity: 0;
  -webkit-transition: all 0.6s cubic-bezier(0.165, 0.84, 0.44, 1);
  transition: all 0.6s cubic-bezier(0.165, 0.84, 0.44, 1);
}

.box:hover {
  -webkit-transform: scale(1.05, 1.05);
  transform: scale(1.05, 1.05);
}

.box:hover::after {
  opacity: 1;
}
</style>
