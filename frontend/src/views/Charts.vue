<template>
    <div>
        <h1>Charts</h1>
        <hr>

        <BasicBarChart />
        <DonutChartVue />
        <LineChartVue />
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import BasicBarChart from '../components/charts/BasicBarChart.vue';
import DonutChartVue from '../components/charts/DonutChart.vue';
import LineChartVue from '../components/charts/LineChart.vue';

    @Component({
        name: 'Charts',
        components: {
            BasicBarChart,
            DonutChartVue,
            LineChartVue
        }
    })
    export default class  extends Vue {
        
    }
</script>

<style scoped>

</style>