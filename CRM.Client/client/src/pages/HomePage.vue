<template>
  <q-page class="q-pa-md">
    <div class="row">
      <div class="text-h2 q-px-xl q-pt-md">Witaj</div>
    </div>
    <div class="row">
      <div class="col-md-3 col-xs-6 q-pa-xl">
        <q-card class="my-card box">
          <router-link to="/clients">
            <q-card-section class="bg-accent">
              <div class="text-h4 text-center q-py-md text-white">Klienci</div>
            </q-card-section>
          </router-link>
        </q-card>
      </div>
      <!-- <div class="col-md-3 col-xs-6 q-pa-xl">
        <q-card class="my-card">
          <q-card-section>
            <div class="text-h4 text-center q-py-md">Klienci</div>
          </q-card-section>
        </q-card>
      </div>
      <div class="col-md-3 col-xs-6 q-pa-xl">
        <q-card class="my-card">
          <q-card-section>
            <div class="text-h4 text-center q-py-md">Klienci</div>
          </q-card-section>
        </q-card>
      </div>
      <div class="col-md-3 col-xs-6 q-pa-xl">
        <q-card class="my-card">
          <q-card-section>
            <div class="text-h4 text-center q-py-md">Klienci</div>
          </q-card-section>
        </q-card>
      </div> -->
    </div>
  </q-page>
</template>

<script>
import { defineComponent } from "vue";

export default defineComponent({
  name: "HomePage",
});
</script>

<style scoped>
.box {
  background: rgb(255, 255, 255);
  background: linear-gradient(
    153deg,
    rgba(255, 255, 255, 1) 0%,
    rgba(179, 233, 238, 1) 42%,
    rgba(62, 136, 144, 1) 100%
  );
  cursor: pointer;
  background-color: #fff;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
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
  -webkit-transform: scale(1.1, 1.1);
  transform: scale(1.1, 1.1);
}

.box:hover::after {
  opacity: 1;
}
</style>
