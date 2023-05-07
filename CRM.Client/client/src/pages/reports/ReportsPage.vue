<template>
  <div class="q-pa-lg">
    <q-card class="q-pa-md shadow-4">
      <q-card-section>
        <div class="text-h4">Raporty</div>
      </q-card-section>
      <q-card-section>
        <div class="row">
          <div class="col-md-6 q-pr-xl">
            <Charts
              id="my-line-id"
              :options="newClientsLineChartOptions"
              :data="newClientsLineChartData"
              v-if="newClientsLineChartData"
            />
          </div>
          <div class="col-md-6 q-pl-xl">
            <Bar id="my-chart-id" :options="newTasksBarChartOptions" :data="newTasksBarChartData" v-if="newTasksBarChartData" />
          </div>
        </div>
      </q-card-section>
    </q-card>
  </div>
</template>
<script>
import { defineComponent, onMounted, ref } from "vue";
import { useReportsStore } from "../../stores/reports";
import { Bar, Line as Charts } from "vue-chartjs";
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LineElement,
  PointElement,
  LinearScale,
} from "chart.js";

ChartJS.register(
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale,
  LineElement,
  PointElement
);

export default {
  components: {
    Bar,
    Charts,
  },
  setup() {
    const reportsStore = useReportsStore();
    const newClientsLineChartData = ref(null);
    const newTasksBarChartData = ref(null);

    const getNewClientsCountData = () => {
      reportsStore.getNewClientsCount().then((response) => {
        const data = response.data;
        const keys = Object.keys(data);
        const values = Object.values(data);
        newClientsLineChartData.value = {
          labels: keys,
          datasets: [
            {
              label: "Liczba nowych klientów",
              data: values,
              fill: false,
              backgroundColor: "#0e5e6f",
              borderColor: "#0e5e6f",
            },
          ],
        };
      });
    }

    const getNewTasksCountData = () => {
      reportsStore.getNewTasksCount().then((response) => {
        const data = response.data;
        const keys = Object.keys(data);
        const values = Object.values(data);
        newTasksBarChartData.value = {
          labels: keys,
          datasets: [
            {
              label: "Liczba nowych zadań",
              data: values,
              fill: false,
              backgroundColor: "#c7b198",
              borderColor: "#c7b198",
            },
          ],
        };
      });
    }

    onMounted(() => {
      getNewClientsCountData();
      getNewTasksCountData();
    });

    return {
      newClientsLineChartData,
      newClientsLineChartOptions: {
        responsive: true,
        scales: {
          y: {
            ticks: {
              stepSize: 1,
            },
            beginAtZero: true,
          },
        },
      },
      newTasksBarChartData,
      newTasksBarChartOptions: {
        responsive: true,
      },
    };
  },
};
</script>
