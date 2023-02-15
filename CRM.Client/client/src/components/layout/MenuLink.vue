<template>
  <q-item
    clickable
    :active="props.name === props.currentRouteName"
    v-ripple
    :to="props.to"
  >
    <q-item-section avatar>
      <q-icon :name="props.icon" />
    </q-item-section>
    <q-item-section>
      {{ props.label }}
    </q-item-section>
  </q-item>
</template>
<script>
export default {
  props: ["name", "currentRouteName", "icon", "label", "to"],
  setup(props) {
    return {
      props,
    };
  },
};
</script>
