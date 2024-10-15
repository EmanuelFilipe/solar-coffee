<template>
    <div>
      <h1 id="ordersTitle">
        Sales Orders
      </h1>
      <hr />
      <table id="sales-orders" class="table" v-if="orders.length">
        <thead>
          <tr>
            <th>CustomerId</th>
            <th>OrderId</th>
            <th>Order Total</th>
            <th>Order Status</th>
            <th>Mark Complete</th>
          </tr>
          <tr v-for="order in orders" :key="order.id">
            <td>
              {{ order.customer.id }}
            </td>
            <td>
              {{ order.id }}
            </td>
            <td>
              {{ getTotal(order) | price }}
            </td>
            <td :class="{ green: order.isPaid }">
              {{ getStatus(order.isPaid) }}
            </td>
            <td>
              <div
                v-if="!order.isPaid"
                class="lni lni-checkmark-circle order-complete green"
                @click="markComplete(order.id)"
              ></div>
            </td>
          </tr>
        </thead>
      </table>
    </div>
  </template>

<script lang="ts">
import { ISalesOrder } from '../types/SalesOrder';
import { OrderService } from '../services/order-service';
import { Component, Vue } from 'vue-property-decorator';

    const orderService = new OrderService()
    @Component
    export default class Orders extends Vue {
        orders: ISalesOrder[] = []
        
        getTotal(order: ISalesOrder) {
            return order.salesOrderItems.reduce((a, b) => {
                const produto = b['product']
                return a + (produto ? produto['price'] * b["quantity"] : 0)
            }, 0);
        }

        getStatus(isPaid: boolean): string {
            return isPaid ? 'Paid in Full' : 'Unpaid' 
        }

        async markComplete(orderId: number): Promise<void> {
            await orderService.markOrderComplete(orderId)
            this.initialize()
        }

        async initialize() {
            this.orders = await orderService.getOrders()
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

.order-complete {
  cursor: pointer;
  text-align: center;
}
</style>