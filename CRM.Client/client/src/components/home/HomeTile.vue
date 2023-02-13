<template>
  <div class="col-md-3 col-xs-6">
    <q-card class="my-card box">
      <router-link :to="props.route">
        <q-card-actions class="bg-accent"> </q-card-actions>
        <q-card-section class="q-py-xl">
          <div class="text-h4 text-center text-black q-py-md">
            {{ props.title }}
          </div>
        </q-card-section>
      </router-link>
    </q-card>
  </div>
</template>
<script>
export default {
  props: ["route", "title"],
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
