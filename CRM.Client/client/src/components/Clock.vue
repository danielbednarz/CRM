<template>
  <div>
    <p style="margin: 0" class="text-black text-h2">
      {{ currentTime.toLocaleTimeString() }}
    </p>
    <p style="margin: 0" class="text-black text-h5">
      {{ currentTime.toLocaleDateString() }}
    </p>
  </div>
</template>
<script>
import { ref, onBeforeUnmount } from "vue";

export default {
  name: "CurrentTimeExample",
  setup() {
    const useCurrentTime = () => {
      const currentTime = ref(new Date());
      const updateCurrentTime = () => {
        currentTime.value = new Date();
      };
      const updateTimeInterval = setInterval(updateCurrentTime, 1000);
      onBeforeUnmount(() => {
        clearInterval(updateTimeInterval);
      });
      return {
        currentTime,
      };
    };
    const { currentTime } = useCurrentTime();

    return { currentTime };
  },
};
</script>
