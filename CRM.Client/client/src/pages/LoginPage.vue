<template>
  <q-page class="row justify-center items-center">
    <login-form></login-form>
  </q-page>
</template>

<script setup>
import { defineComponent } from "vue";
import LoginForm from "../components/LoginForm.vue";
import { ref } from "vue";
import { useCounterStore } from "../stores/example-store";

const storeCounter = useCounterStore();
</script>
