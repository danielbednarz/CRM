<template>
  <div class="q-ma-md">
    <div class="row">
      <div class="col-md-8 col-xs-12">
        <div class="text-h4">
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
        </div>
        <div class="text-h6">NIP: {{ client.nip }}</div>
      </div>
    </div>
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
  </div>
</template>
<script>
import { ref } from "vue";
export default {
  props: {
    client: {
      type: Object,
    },
  },
  setup(props) {
    return {
      props,
    };
  },
};
</script>
<style scoped></style>
