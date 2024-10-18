import { make } from "vuex-pathify";
import { IInventoryTimeline } from "@/types/InventoryGraph";
import { InventoryService } from "@/services/inventory-service";

class GlobalStore {
  snapshotTimeline: IInventoryTimeline = {
    productInventorySnapshots: [],
    timeline: []
  };

  isTimelineBuilt: boolean = false;
}

const state = new GlobalStore();

//const mutations = make.mutations(state);

const mutations = {
  ...make.mutations(state),
  SET_IS_TIMELINE_BUILT(state: GlobalStore, value: boolean) {
    console.log('::mutations::value', value)
    console.log('::mutations::GlobalStore', state)
    state.isTimelineBuilt = value;
    console.log('::mutations::state.isTimelineBuilt', state.isTimelineBuilt)
  }
}

const actions = {
    // @ts-ignore
  async assignSnapshots({ commit }) {
    const inventoryService = new InventoryService();
    let res = await inventoryService.getSnapshotHistory();

    let timeline: IInventoryTimeline = {
      productInventorySnapshots: res.productInventorySnapshots,
      timeline: res.timeline
    };

    commit('SET_SNAPSHOT_TIMELINE', timeline);
    console.log('::antes de SET_IS_TIMELINE_BUILT', state.isTimelineBuilt)
    commit('SET_IS_TIMELINE_BUILT', true);
    },
};

const getters = {};

export default {
  state,
  mutations,
  actions,
  getters
};