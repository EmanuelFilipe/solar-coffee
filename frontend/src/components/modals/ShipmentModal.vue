<template>
    <div>
        <solar-modal>
            <template v-slot:header>
                Receive Shipment
            </template>
            <template v-slot:body>
                <label for="product">Product Received:</label>
                <select class="shipmentItems" id="product"
                 v-model="selectedProduct">
                    <option disabled value="">Please select one</option>
                    <option v-for="item in inventory" :value="item" 
                     :key="item.product.id"
                    >
                        {{ item.product.name }}
                    </option>
                </select>
                
                <label for="qtyReceived">Quantity Received:</label>
                <input type="number" id="qtyReceived" v-model="qtyReceived">
            </template>
            <template v-slot:footer>
                <solar-button type="button" aria-label="save new shipment" @button:click="save">
                    Save Received Shipment
                </solar-button>
                <solar-button type="button" aria-label="Close modal" @button:click="close">
                    Close
                </solar-button>
            </template>
        </solar-modal>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator'
    import SolarButton from '../SolarButton.vue'
    import SolarModal from './SolarModal.vue'
    import { IProduct, IProductInventory } from '@/types/Product'
    import { IShipment } from '@/types/Shipment'

    @Component({
        name: 'ShipmentModal',
        components: { 
            SolarButton,
            SolarModal
        }
    })

    export default class ShipmentModal extends Vue {
        @Prop({ required: true, type: Array as () => IProductInventory[] })
        inventory!: IProductInventory[];

        selectedProduct: IProduct = {
            id: 0,
            name: '',
            description: '',
            createdOn: new Date(),
            updatedOn: new Date(),
            price: 0,
            isTaxable: false,
            isArchived: false
        };

        qtyReceived: number = 0;

        close() {
            this.$emit('close')
        }

        save() {
            let shipment: IShipment = {
                productId: this.selectedProduct.id,
                adjustment: this.qtyReceived
            }

            this.$emit('save:shipment', shipment)
        }
    }
</script>

<style scoped>

</style>