<template>
  <div class="col-md-3 col-xs-6">
    <q-card class="my-card box">
      <router-link :to="props.route">
        <q-card-actions class="bg-accent"> </q-card-actions>
        <q-card-section class="q-py-xl">
          <div class="text-h4 text-center text-black">
            <i :class="icon"></i>
          </div>
          <div class="text-h4 text-center text-black title">
            {{ props.title }}
          </div>
        </q-card-section>
      </router-link>
    </q-card>
  </div>
</template>
<script>
export default {
  props: ["route", "title", "icon"],
  setup(props) {
    return {
      props,
    };
  },
};
</script>
<style scoped>
.box {
  background: rgb(255, 255, 255);
  cursor: pointer;
  background-color: #fff;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
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
  transition: all 0.6s cubic-bezier(0.165, 0.84, 0.44, 1);
}

.box:hover {
  transform: scale(1.02, 1.02);
}

.box:hover::after {
  opacity: 1;
}

.title {
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>
