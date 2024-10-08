<template>
    <div>
        <solar-modal>
            <template v-slot:header>
                Add New Product
            </template>
            <template v-slot:body>
                <ul class="newProduct">
                    <li>
                        <label for="isTaxable">Is this product taxable?</label>
                        <input type="checkbox" id="isTaxable" v-model="newProduct.isTaxable">
                    </li>
                    <li>
                        <label for="productName">Name</label>
                        <input type="text" id="productName" v-model="newProduct.name">
                    </li>
                    <li>
                        <label for="productDesc">Description</label>
                        <input type="text" id="productDesc" v-model="newProduct.description">
                    </li>
                    <li>
                        <label for="productPrice">Price (USD)</label>
                        <input type="number" id="productPrice" v-model="newProduct.price">
                    </li>
                </ul>
            </template>
            <template v-slot:footer>
                <solar-button type="button" aria-label="save new item" @button:click="save">
                    Save Product
                </solar-button>
                <solar-button type="button" aria-label="Close modal" @button:click="close">
                    Close
                </solar-button>
            </template>
        </solar-modal>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator'
    import SolarButton from '../SolarButton.vue'
    import SolarModal from './SolarModal.vue'
    import { IProduct } from '@/types/Product';

    @Component({
        name: 'NewProductModal',
        components: { 
            SolarButton,
            SolarModal
        }
    })

    export default class NewProductModal extends Vue {
        newProduct: IProduct = {
            id: 0,
            name: '',
            description: '',
            createdOn: new Date(),
            updatedOn: new Date(),
            price: 0,
            isTaxable: false,
            isArchived: false
        };

        close() {
            this.$emit('close')
        }

        save() {
            this.$emit('save:product', this.newProduct)
        }
    }
</script>

<style scoped lang="scss">
    .newProduct {
        list-style: none;
        padding: 0;
        margin: 0;
        
        input {
            width: 100%;
            height: 1.8rem;
            margin-bottom: 1.2rem;
            font-size: 1.1rem;
            line-height: 1.3rem;
            padding: 0.2rem;
            color: #555;
        }
        
        label {
            font-weight: bold;
            display: block;
            margin-bottom: 0.3rem;
        }
    }

    #isTaxable {
        width: auto;
        width: 20px;
        height: 20px;
    }

</style>