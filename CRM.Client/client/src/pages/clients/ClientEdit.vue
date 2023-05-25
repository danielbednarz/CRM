<template>
  <div class="q-pa-lg">
    <div class="q-gutter-y-md">
      <q-card>
        <q-form
          @submit="onSubmit"
          class="q-pa-xl q-gutter-sm"
          v-if="clientsStore.client"
        >
          <div class="text-h4 q-ml-lg q-mb-md">Edytuj klienta</div>
          <div class="row">
            <div class="col-md-10 col-xs-12 q-px-md">
              <q-input
                v-model="clientsStore.client.name"
                label="Nazwa klienta"
                :rules="[
                  (val) => val.length > 1 || 'Nazwa klienta jest za krótka',
                  (val) => val.length < 100 || 'Nazwa klienta jest za długa',
                ]"
              />
            </div>
            <div class="col-md-2">
              <q-checkbox
                v-model="clientsStore.client.isActive"
                class="q-mt-lg q-ml-md"
                label="Czy aktywny?"
              />
            </div>
          </div>
          <div class="row">
            <div class="col-md-6 col-xs-12 q-px-md">
              <q-input
                v-model="clientsStore.client.nip"
                label="Nip"
                :rules="[
                  (val) =>
                    (val.length >= 10 && val.length <= 14) || 'Błędny numer NIP',
                ]"
              />
            </div>
          </div>
          <div class="row">
            <div class="col-md-6 col-xs-12 q-px-md">
              <q-input
                v-model="clientsStore.client.country"
                label="Kraj"
                :rules="[(val) => val.length != 0 || 'Pole nie może być puste']"
              />
            </div>
            <div class="col-md-6 col-xs-12 q-px-md">
              <span class="label flex q-pt-sm">Rating</span>
              <q-rating
                class="q-pt-xs"
                v-model="clientsStore.client.rating"
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
              <q-input
                v-model="clientsStore.client.city"
                label="Miejscowość"
                :rules="[(val) => val.length != 0 || 'Pole nie może być puste']"
              />
            </div>
            <div class="col-md-6 col-xs-12 q-px-md">
              <q-input v-model="clientsStore.client.krs" label="Krs" />
            </div>
          </div>
          <div class="row">
            <div class="col-md-6 col-xs-12 q-px-md">
              <q-input v-model="clientsStore.client.street" label="Ulica" />
            </div>
            <div class="col-md-6 col-xs-12 q-px-md">
              <q-input v-model="clientsStore.client.regon" label="REGON" />
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
import { ref, onMounted } from "vue";
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

    onMounted(() => clientsStore.getClientById(route.params.id));

    return {
      clientsStore,
      router,
      route,
      async onSubmit() {
        await clientsStore.editClient().then(
          () => {
            $q.notify({
              type: "info",
              message: `Zapisano pomyślnie`,
            });
            router.push(`/clients/${clientsStore.client.id}`);
          },
          (error) => {
            $q.notify({
              type: "negative",
              message: `Błąd przy próbie zapisu`,
            });
          }
        );
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
