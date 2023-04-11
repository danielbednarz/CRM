<template>
  <div>
    <q-card class="q-px-md q-pt-md">
      <div class="q-px-lg">
        <div class="flex">
          <div class="text-h5 q-pt-sm">Historia</div><q-toggle v-model="visible" class="q-mb-md q-mt-xs" />

        </div>
        <q-slide-transition>
          <div v-show="visible">
          <q-timeline color="secondary">
            <q-timeline-entry
              v-for="(item, index) in props.history"
              :key="index"
              :title="historyStep(item.fromStep, item.toStep)"
              :subtitle="formattedDateTime(item.createdDate)"
            >
            </q-timeline-entry>
          </q-timeline>
        </div>
        </q-slide-transition>
      </div>
    </q-card>
  </div>
</template>

<script>
import { computed, ref } from "vue";
export default {
  props: {
    history: {
      type: Object,
      required: true,
    },
  },
  setup(props) {
    const historyStep = computed(() => {
      return (fromStep, toStep) => {
        if (fromStep == "") fromStep = "Rejestracja";
        return `${fromStep} -> ${toStep}`;
      };
    });
    const formattedDateTime = computed(() => {
      return (createdDate) => {
        return `${new Date(createdDate).toLocaleDateString(
          "pl-PL"
        )}, ${new Date(createdDate).toLocaleTimeString("pl-PL")}`;
      };
    });

    return {
      props,
      historyStep,
      formattedDateTime,
      visible: ref(false)
    };
  },
};
</script>
