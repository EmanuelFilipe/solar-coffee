<template>
  <div>
    <h1 id="customersTitle">Manage Customers</h1>
    <hr />
    <div class="customers-actions">
      <solar-button @button:click="showNewCustomerModal"> Add Customer </solar-button>
    </div>
    <table id="customers" class="table">
      <thead>
        <tr>
          <th>Customers</th>
          <th>Address</th>
          <th>State</th>
          <th>Since</th>
          <th>Delete</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="customer in customers" :key="customer.id">
          <td> {{ `${customer.firstName} ${customer.lastName}` }}</td>
          <td> 
            {{ `${customer.primaryAddress.addressLine1} ${customer.primaryAddress.addressLine2}` }}
          </td>
          <td> {{ customer.primaryAddress.state }}</td>
          <td> {{ customer.createdOn | formatDate}}</td>
          <td>
            <div class="lni lni-cross-circle delete-item"
              @click="deleteCustomer(customer.id)">
            </div>
          </td>
        </tr>
      </tbody>
    </table>
    <new-customer-modal v-if="isCustomerModalVisible" 
      @save:customer="saveNewCustomer"
      @close="closeModal"
    />
  </div>
</template>

<script lang="ts">
import { ICustomer } from "../types/Customer";
import SolarButton from "../components/SolarButton.vue";
import { Component, Vue } from "vue-property-decorator";
import { CustomerService } from '../../services/customer-service'
import NewCustomerModal from '../components/modals/NewCustomerModal.vue'

const customerService = new CustomerService()

@Component({
  name: "Customers",
  components: { SolarButton, NewCustomerModal },
})
export default class Customers extends Vue {
  isCustomerModalVisible: boolean = false
  customers: ICustomer[] = []

  async initialize(): Promise<void> {
    this.customers = await customerService.getCustomers()
  }

  async created(): Promise<void>{
    await this.initialize();
  }
  
  showNewCustomerModal() {
    this.isCustomerModalVisible = true
  }

  closeModal() {
    this.isCustomerModalVisible = false
  }
  
  async saveNewCustomer(newCustomer: ICustomer) {
    await customerService.addCustomers(newCustomer)
    this.isCustomerModalVisible = false
    await this.initialize()
  }

  async deleteCustomer(id: number): Promise<void> {
    await customerService.deleteCustomer(id);
    await this.initialize()
  }

}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";

.customers-actions {
  display: flex !important;
  margin-bottom: 0.8rem;

  div {
    margin-right: 0.8rem;
  }
}
</style>