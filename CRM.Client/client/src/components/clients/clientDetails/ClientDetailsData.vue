<template>
  <div class="q-ma-md">
    <div class="row" v-if="client">
      <div class="col-md-8 col-xs-12">
        <div class="text-h4">
          {{ client.name }}
          <q-icon
            v-if="client.isActive"
            name="fa-solid fa-square-check"
            color="green"
          >
            <q-tooltip> Klient jest aktywny </q-tooltip>
          </q-icon>
          <q-icon
            v-if="!client.isActive"
            name="fa-solid fa-square-xmark"
            size="md"
            color="red"
          >
            <q-tooltip> Klient nie jest aktywny </q-tooltip>
          </q-icon>
        </div>
        <div class="text-h6 q-my-sm">NIP: {{ client.nip }}</div>
      </div>
      <div class="col-md-4">
        <div class="flex justify-end q-gutter-md">
          <q-btn
            outline
            class="q-py-sm"
            color="secondary"
            label="Edytuj klienta"
            icon-right="fa-solid fa-pen"
            @click="moveToClientEdit"
          ></q-btn>
          <q-btn
            outline
            class="q-py-sm"
            color="negative"
            label="Usuń klienta"
            icon-right="fa-solid fa-trash"
            @click="confirm = true"
          ></q-btn>
        </div>
      </div>
    </div>
    <q-separator />
    <div class="row q-mt-md">
      <div class="col-md-8 col-xs-12">
        <div class="row">
          <div class="col-md-5 col-xs-12">Kraj:</div>
          <div class="col-md-7 col-xs-12">{{ client.country }}</div>
        </div>
        <div class="row q-my-sm">
          <div class="col-md-5 col-xs-12">Miejscowość:</div>
          <div class="col-md-7 col-xs-12">{{ client.city }}</div>
        </div>
        <div class="row q-my-sm">
          <div class="col-md-5 col-xs-12">Ulica i numer budynku:</div>
          <div class="col-md-7 col-xs-12">
            {{ client.street }} {{ client.buildingNumber }}
          </div>
        </div>
      </div>
      <div class="col-md-4 col-xs-12">
        <div class="row q-my-sm">
          <div class="col-md-6 col-xs-12">Ocena klienta:</div>
          <div class="col-md-6 col-xs-12">
            <q-rating
              v-model="props.client.rating"
              max="5"
              size="1.25em"
              color="yellow"
              icon="star_border"
              icon-selected="star"
              no-dimming
              readonly
            >
            </q-rating>
            <q-tooltip> {{ props.client.rating }}</q-tooltip>
          </div>
        </div>
        <div class="row q-my-sm">
          <div class="col-md-6 col-xs-12">Krs:</div>
          <div class="col-md-6 col-xs-12">{{ client.krs }}</div>
        </div>
        <div class="row q-my-sm">
          <div class="col-md-6 col-xs-12">REGON:</div>
          <div class="col-md-6 col-xs-12">{{ client.regon }}</div>
        </div>
      </div>
    </div>
    <q-separator />
    <div class="row q-mt-md">
      <div class="col-md-12 q-mb-md">
        <div class="text-h5">Dane kontaktowe</div>
      </div>
      <div class="col-md-6 col-xs-12">
        <div class="text-h6">Telefon</div>
        <q-separator class="q-mr-md q-mb-sm" />
        <p v-if="client.clientPhoneNumbers.length === 0">Brak</p>
        <p v-for="(item, index) in client.clientPhoneNumbers" :key="index">
          {{ item.phoneNumber }}
        </p>
      </div>
      <div class="col-md-6 col-xs-12">
        <div class="text-h6">Email</div>
        <q-separator class="q-mb-sm" />
        <p v-if="client.clientPhoneNumbers.length === 0">Brak</p>
        <p v-for="(item, index) in client.clientEmails" :key="index">
          {{ item.email }}
        </p>
      </div>
    </div>
  </div>
  <q-dialog v-model="confirm" persistent>
    <q-card>
      <q-card-section class="row items-center">
        <q-icon
          name="fa-solid fa-triangle-exclamation"
          color="primary"
          size="lg"
        />
        <span class="q-ml-sm" style="font-size: 18px"
          >Czy na pewno chcesz usunąć klienta?</span
        >
      </q-card-section>

      <q-card-actions align="right">
        <q-btn flat label="Anuluj" color="primary" v-close-popup />
        <q-btn
          flat
          label="Tak"
          color="primary"
          @click="deleteClient()"
          v-close-popup
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>
<script>
import { ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useClientsStore } from "../../../stores/clients";
import { useQuasar } from "quasar";

export default {
  props: {
    client: {
      type: Object,
    },
  },
  setup(props) {
    const route = useRoute();
    const router = useRouter();
    const clientsStore = useClientsStore();
    const $q = useQuasar();

    return {
      props,
      route,
      router,
      confirm: ref(false),
      moveToClientEdit() {
        router.push(`/clients/edit/${route.params.id}`);
      },
      async deleteClient() {
        await clientsStore.deleteClient().then(
          () => {
            $q.notify({
              type: "info",
              message: `Klient usunięty pomyślnie`,
            });
            router.push(`/clients`);
          },
          (error) => {
            $q.notify({
              type: "negative",
              message: `Błąd przy próbie usunięcia`,
            });
          }
        );
      },
    };
  },
};
</script>
<style scoped>
p {
  color: rgb(114, 114, 114);
}
</style>
