<template>
  <div class="inventoty-container">
    <h1 id="inventoryTitle">Inventory Dashboard</h1>
    <hr />

    <div id="inventory-actions">
      <solar-button id="addNewItem" @button:click="showNewProductModal">
        Add New Item
      </solar-button>
      <solar-button id="receiveShipmentModal" @button:click="showShipmentModal">
        Receive Shipment
      </solar-button>
    </div>
    <table id="inventoryTable" class="table">
      <thead>
        <tr>
          <th>Item</th>
          <th>Quantity On-hand</th>
          <th>Unit Price</th>
          <th>Taxable</th>
          <th>Delete</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="item in inventory" :key="item.id">
          <td>
            {{ item.product.name }}
          </td>
          <td :class="`${applyColor(item)}`">
            {{ item.quantityOnHand }}
          </td>
          <td>
            {{ item.product.price | price }}
          </td>
          <td>
            {{ item.product.isTaxable ? "Yes" : "No" }}
          </td>
          <td>
            <div class="lni lni-cross-circle delete-item"
              @click="archiveProduct(item.product.id)"
            >
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <!-- MODALS -->
    <new-product-modal v-if="isNewProductVisible" 
      @save:product="saveNewProduct" 
      @close="closeModals"
    />
    <shipment-modal v-if="isShipmentVisible" 
      :inventory="inventory" 
      @close="closeModals"
      @save:shipment="saveNewShipment"
    />
  </div>
</template>

<script lang="ts">
import { IProduct, IProductInventory } from "../types/Product";
import { Component, Vue } from "vue-property-decorator";
import SolarButton from "../components/SolarButton.vue";
import NewProductModal from "../components/modals/NewProductModel.vue";
import ShipmentModal from "../components/modals/ShipmentModal.vue";
import { IShipment } from "../types/Shipment";
import { InventoryService } from '../../services/inventory-service'
import { ProductService } from '../../services/product-service'

const inventoryService = new InventoryService()
const productService = new ProductService()

@Component({
  name: "Inventory",
  components: { 
    SolarButton,
    NewProductModal,
    ShipmentModal
   },
})
export default class Inventory extends Vue {
  isNewProductVisible: boolean = false;
  isShipmentVisible: boolean = false;

  inventory: IProductInventory[] = []

  closeModals() {
    this.isShipmentVisible = false;
    this.isNewProductVisible = false;
  }

  showNewProductModal() {
    this.isNewProductVisible = true
  }

  showShipmentModal() {
    this.isShipmentVisible = true
  }

  applyColor(item: IProductInventory): string {
    if (item.quantityOnHand <= 0) 
      return 'red'

    if (Math.abs(item.idealQuantity - item.quantityOnHand) > 8)
      return 'yellow'

    return 'green'
  }

  async archiveProduct(productId: number) {
    await productService.archive(productId);
    await this.initialize()
  }

  async saveNewProduct(newProduct: IProduct) {
    await productService.save(newProduct)
    this.isNewProductVisible = false
    await this.initialize()
  }

  async saveNewShipment(shipment: IShipment) {
    await inventoryService.updateInventoryQuantity(shipment);
    this.isShipmentVisible = false
    await this.initialize();
  }

  async initialize() {
    this.inventory = await inventoryService.getInventory()
  }

  async created() {
    await this.initialize()
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";

.green {
  font-weight: bold;
  color: $solar-green;
}

.yellow {
  font-weight: bold;
  color: $solar-yellow;
}

.red {
  font-weight: bold;
  color: $solar-red;
}

#inventory-actions {
  display: flex !important;
  margin-bottom: 0.8rem;
}

</style>