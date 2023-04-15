<template>
  <div class="row q-pa-lg q-gutter-md">
    <menu-tile route="/administration/users" title="Użytkownicy" icon="fa-solid fa-user-group" color="bg-secondary" />
    <menu-tile route="/tasks" title="Grupy" icon="fa-solid fa-people-line" color="bg-secondary" />
  </div>
</template>

<script>
import { defineComponent } from "vue";
import MenuTile from "../../components/MenuTile.vue";

export default {
  components: {
    MenuTile,
  },
  setup() {},
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
.test {
  color: #c8c6c6;
}
</style>
