<template>
  <div class="inventoty-container">
    <h1 id="inventoryTitle">Inventory Dashboard</h1>
    <hr />

    <div id="inventory-actions">
        <solar-button id="addNewItem" @click.native="showNewProductModal" >
            Add New Item
        </solar-button>
        <solar-button id="receiveShipmentModal" @click.native="showShipmentModal" >
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
                <td>
                    {{ item.quantityOnHand }}
                </td>
                <td>
                    {{ item.product.price | price }}
                </td>
                <td>
                    {{ item.product.isTaxable ? 'Yes' : 'No' }}
                </td>
                <td>
                    <div>X</div>
                </td>
            </tr>

        </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { IProductInventory } from "../types/Product";
import { Component, Vue } from "vue-property-decorator";
import SolarButton from '../components/SolarButton.vue' ;

@Component({
  name: "Inventory",
  components: {SolarButton}
})
export default class Inventory extends Vue {
  inventory: IProductInventory[] = [
    {
        id: 1,
        product: {
            id: 1,
            name: 'Some product',
            description: 'Good stuff',
            price: 100,
            createdOn: new Date(),
            updatedOn: new Date(),
            isTaxable: true,
            isArchived: false
        },
        quantityOnHand: 100,
        idealQuantity: 100
    },
    {
        id: 2,
        product: {
            id: 2,
            name: 'Another product',
            description: 'AnotherGood stuff',
            price: 100,
            createdOn: new Date(),
            updatedOn: new Date(),
            isTaxable: false,
            isArchived: false
        },
        quantityOnHand: 40,
        idealQuantity: 20
    },

  ];

  showNewProductModal() {

  }

  showShipmentModal() {
    
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
</style>