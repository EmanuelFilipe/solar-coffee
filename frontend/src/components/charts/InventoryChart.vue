<template>
  <div v-if="isTimelineBuilt">
    <apexchart
      type="area"
      width="100%"
      height="300"
      :options="options"
      :series="series"
    ></apexchart>
  </div>
</template>

<script lang="ts">
import { Component, Vue, Watch } from "vue-property-decorator";
import { IInventoryTimeline } from "../../types/InventoryGraph";
import { Get, Sync } from "vuex-pathify";
import VueApexCharts from "vue-apexcharts";
import { InventoryService } from "../../services/inventory-service";
import moment from "moment";

const inventoryService = new InventoryService()

Vue.component("apexchart", VueApexCharts);

@Component({
  name: "InventoryChart",
})
export default class InventoryChart extends Vue {
  // @ts-ignore
  @Sync("snapshotTimeline")
  snapshotTimeline?: IInventoryTimeline;

  // @ts-ignore
  @Get("isTimelineBuilt")
  isTimelineBuilt?: boolean = false;

  get options() {
    console.log("options", this.snapshotTimeline);
    return {
      dataLabels: { enabled: false },
      fill: {
        type: "gradient",
      },
      stroke: {
        curve: "smooth",
      },
      xaxis: {
        // categories: this.snapshotTimeline?.timeline
        //             .map(t => moment(t).format('dd HHMMss')),
         //categories: this.snapshotTimeline?.timeline
         //            .map(t => moment(t).format('dd HHMM')),
        categories: this.snapshotTimeline?.timeline || [],
        type: "datetime",
      },
    };
  }

  get series() {
    return (
      this.snapshotTimeline?.productInventorySnapshots.map((snapshot) => ({
        name: snapshot.productId,
        data: snapshot.quantityOnHand,
      })) || []
    );
  }

  async created() {
    //await this.$store.dispatch("assignSnapshots");
    await inventoryService.getSnapshotHistory()
      .then(res => { 
        this.snapshotTimeline = res
        this.isTimelineBuilt = true
      })
  }

  // @Watch('isTimelineBuilt')
  // onIsTimelineBuiltChanged(newVal: boolean) {
  //   console.log('isTimelineBuilt changed:', newVal);
  // }
}
</script>
