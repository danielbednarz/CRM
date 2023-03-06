<template>
  <div class="q-pa-lg">
    <div class="q-gutter-y-md">
      <q-card>
        <q-form @submit="onSubmit(clientToAdd)" class="q-pa-xl q-gutter-sm">
          <div class="text-h4 q-ml-lg q-mb-md">Dodaj klienta</div>
          <div class="row">
            <div class="col-md-10 col-xs-12 q-px-md">
              <q-input v-model="clientToAdd.name" label="Nazwa klienta" />
            </div>
            <div class="col-md-2">
              <q-checkbox
                v-model="clientToAdd.isActive"
                class="q-mt-lg q-ml-md"
                label="Czy aktywny?"
              />
            </div>
          </div>
          <div class="row">
            <div class="col-md-6 col-xs-12 q-px-md">
              <q-input v-model="clientToAdd.nip" label="Nip" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-6 col-xs-12 q-px-md">
              <q-input v-model="clientToAdd.country" label="Kraj" />
            </div>
            <div class="col-md-6 col-xs-12 q-px-md">
              <span class="label flex q-pt-sm">Rating</span>
              <q-rating
                class="q-pt-xs"
                v-model="clientToAdd.rating"
                max="5"
                size="2em"
                color="yellow"
                icon="star_border"
                icon-selected="star"
                no-dimming
              />
            </div>
          </div>
          <div class="row">
            <div class="col-md-6 col-xs-12 q-px-md">
              <q-input v-model="clientToAdd.city" label="Miejscowość" />
            </div>
            <div class="col-md-6 col-xs-12 q-px-md">
              <q-input v-model="clientToAdd.krs" label="Krs" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-4 col-xs-12 q-px-md">
              <q-input v-model="clientToAdd.street" label="Ulica" />
            </div>
            <div class="col-md-2 col-xs-12 q-px-md">
              <q-input
                v-model="clientToAdd.buildingNumber"
                label="Numer budynku"
              />
            </div>
            <div class="col-md-6 col-xs-12 q-px-md">
              <q-input v-model="clientToAdd.regon" label="REGON" />
            </div>
          </div>
          <div class="row q-pb-md">
            <div class="col-xs-12 q-pl-md q-pt-md q-gutter-md">
              <q-btn color="primary" label="Zapisz" type="submit"></q-btn>
              <q-btn
                color="secondary"
                label="Anuluj"
                @click="$router.go(-1)"
              ></q-btn>
            </div>
          </div>
        </q-form>
      </q-card>
    </div>
  </div>
</template>
<script>
import { ref, onMounted, reactive } from "vue";
import { useRouter, useRoute } from "vue-router";
import { useClientsStore } from "../../stores/clients";
import { useQuasar } from "quasar";

export default {
  components: {},
  setup() {
    const clientsStore = useClientsStore();
    const router = useRouter();
    const route = useRoute();
    const $q = useQuasar();

    let clientToAdd = ref({
      name: "",
      nip: "",
      krs: "",
      regon: "",
      rating: 5,
      country: "",
      city: "",
      street: "",
      buildingNumber: "",
      isActive: true,
    });

    return {
      clientsStore,
      router,
      route,
      clientToAdd,
      async onSubmit() {
        clientsStore.addClient(this.clientToAdd).then((response) => {
          console.log(response);
        });
      },
    };
  },
};
</script>
<style scoped>
.label {
  color: rgb(146, 146, 146);
}
</style>
