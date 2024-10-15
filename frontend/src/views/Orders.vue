<template>
    <div>
        <h1 id="ordersTitle">
            Sales Orders
        </h1>
        <hr>
        <table id="sales-orders" class="table" v-if="Orders.length">
            <thead>

            </thead>
        </table>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component
    export default class Orders extends Vue {
     
        
        // initialize() {
        //     this.orders = await orderService.getOrders()
        // }

        // async created() {
        //     this.initialize()
        // }
    }
</script>

<style scoped lang="scss">

</style>