<template>
  <div>
    <h1 id="invoiceTitle">Create Invoice</h1>
    <hr />

    <div class="invoice-step" v-if="invoiceStep === 1">
      <h2>Step 1: Select Customer</h2>
      <div v-if="customers.length" class="invoice-step-detail">
        <label for="customer">Customer:</label>
        <select
          id="customer"
          class="invoice-customers"
          v-model="selectedCustomerId"
        >
          <option disabled value="">Please select a Customer</option>
          <option v-for="c in customers" :key="c.id" :value="c.id">
            {{ c.firstName + " " + c.lastName }}
          </option>
        </select>
      </div>
    </div>
    <div class="invoice-step" v-if="invoiceStep === 2">
      <h2>Step 2: Create Order</h2>
      <div v-if="inventory.length" class="invoice-step-detail">
        <label for="product">Product:</label>
        <select id="product" v-model="newItem.product" class="invoiceLineItem">
          <option disabled value="">Please select a Product</option>
          <option v-for="i in inventory" :value="i.product" :key="i.product.id">
            {{ i.product.name }}
          </option>
        </select>
        <label for="quantity">Quantity:</label>
        <input id="quantity" type="number" v-model="newItem.quantity" min="0" />
      </div>
      <div class="invoice-item-actions">
        <solar-button
          :disabled="!newItem.product || !newItem.quantity"
          @button:click="addLineItem"
        >
          Add Line Item
        </solar-button>
        <solar-button
          :disabled="!lineItems.length"
          @button:click="finalizeOrder"
        >
          Finalize Order
        </solar-button>
      </div>
      <div class="invoice-order-list" v-if="lineItems.length">
        <div class="runningTotal">
          <h3>Running Total:</h3>
          {{ runningTotal | price }}
        </div>
        <hr />
        <table class="table">
          <thead>
            <tr>
              <th>Product</th>
              <th>Description</th>
              <th>Qty.</th>
              <th>Price</th>
              <th>Subtotal</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="li in lineItems" :key="`index_${li?.product?.id}`">
              <td>{{ li?.product?.name }}</td>
              <td>{{ li?.product?.description }}</td>
              <td>{{ li.quantity }}</td>
              <td>{{ li?.product?.price }}</td>
              <td>
                {{ ((li?.product?.price ?? 0) * Number(li.quantity)) | price }}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
    <div class="invoice-step" v-if="invoiceStep === 3">
      <h2>Step 3: Review and Submit</h2>
      <solar-button type="button" @button:click="submitInvoice">Submit Invoice</solar-button>
      <hr />

      <div class="invoice-step-detail" id="invoice" ref="invoice">
        <div class="invoice-logo">
          <img
            id="imgLogo"
            alt="Solar Coffee logo"
            src="@/assets/images/solar_coffee_logo.png"
          />
          <h3>1337 Solar Lane</h3>
          <h3>San Somewhere, CA 90000</h3>
          <h3>USA</h3>

          <div class="invoice-order-list" v-if="lineItems.length">
            <div class="invoice-header">
              <h3>Invoice: {{ new Date() | formatDate }}</h3>
              <h3>
                Customer:
                {{
                  this.selectedCustomer?.firstName +
                  " " +
                  this.selectedCustomer?.lastName
                }}
              </h3>
              <h3>
                Address:
                {{ this.selectedCustomer?.primaryAddress.addressLine1 }}
              </h3>
              <h3 v-if="this.selectedCustomer?.primaryAddress.addressLine2">
                {{ this.selectedCustomer.primaryAddress.addressLine2 }}
              </h3>
              <h3>
                {{ this.selectedCustomer?.primaryAddress.city }},
                {{ this.selectedCustomer?.primaryAddress.state }},
                {{ this.selectedCustomer?.primaryAddress.postalCode }}
              </h3>
              <h3>
                {{ this.selectedCustomer?.primaryAddress.country }}
              </h3>
            </div>
            <table class="table">
                <thead>
                    <th>Product</th>
                    <th>Description</th>
                    <th>Qty.</th>
                    <th>Price</th>
                    <th>Subtotal</th>
                </thead>
                <tbody>
                    <tr v-for="li in lineItems" :key="`index_${li?.product?.id}`">
                        <td>{{ li?.product?.name }}</td>
                        <td>{{ li?.product?.description }}</td>
                        <td>{{ li.quantity }}</td>
                        <td>{{ li?.product?.price }}</td>
                        <td>
                          {{ ((li?.product?.price ?? 0) * Number(li.quantity)) | price }}
                        </td>
                    </tr>
                    <tr>
                        <th colspan="4"></th>
                        <th>Grand Total</th>
                    </tr>
                    <tfoot>
                        <tr>
                          <td colspan="4" class="due">Balance due upon receipt:</td>
                          <td class="price-final">{{ runningTotal | price }}</td>
                        </tr>
                    </tfoot>
                </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
    <hr />
    <div class="invoice-steps-actions">
      <solar-button @button:click="prev" :disabled="!canGoPrev">
        Previous
      </solar-button>
      <solar-button @button:click="next" :disabled="!canGoNext">
        Next
      </solar-button>
      <solar-button @button:click="startOver"> Start Over </solar-button>
    </div>
  </div>
</template>

<script lang="ts">
import { ICustomer } from "../types/Customer";
import { IInvoice, ILineItem } from "../types/Invoice";
import { IProductInventory } from "@/types/Product";
import { CustomerService } from "../../services/customer-service";
import { InventoryService } from "../../services/inventory-service";
import { InvoiceService } from "../../services/invoice-service";
import { Component, Vue } from "vue-property-decorator";
import SolarButton from "@/components/SolarButton.vue";
import FiltersMixin from "../mixins/filters";

const customerService = new CustomerService();
const inventoryService = new InventoryService();
const invoiceService = new InvoiceService();

@Component({
  name: "CreateInvoice",
  components: { SolarButton },
  mixins: [FiltersMixin],
})
export default class CreateInvoice extends Vue {
  invoiceStep: number = 1;

  invoice: IInvoice = {
    createdOn: new Date(),
    customerId: 0,
    lineItems: [],
    updatedOn: new Date(),
  };
  customers: ICustomer[] = [];
  selectedCustomerId: number = 0;
  inventory: IProductInventory[] = [];
  lineItems: ILineItem[] = [];
  newItem: ILineItem = {
    product: undefined,
    quantity: 0,
  };

  //computed
  get canGoPrev() {
    return this.invoiceStep !== 1;
  }

  get canGoNext() {
    // have to be selected a customer to proced for next step
    if (this.invoiceStep === 1) return this.selectedCustomerId !== 0;
    if (this.invoiceStep === 2) return this.lineItems.length;
    if (this.invoiceStep === 3) return false;

    return false;
  }

  get runningTotal(): number {
    return this.lineItems.reduce((total, item) => {
      const product = item["product"];
      const price = Number(product?.price);
      const quantity = Number(item?.quantity);

      if (product && !isNaN(price) && !isNaN(quantity)) {
        return total + price * quantity;
      } else {
        console.warn("Invalid item detected:", item);
      }
      return total;
    }, 0);
  }

  get selectedCustomer() {
    return this.customers.find((c) => c.id === this.selectedCustomerId);
  }

  prev(): void {
    if (this.invoiceStep === 1) return;
    this.invoiceStep -= 1;
  }

  next(): void {
    if (this.invoiceStep === 3) return;
    this.invoiceStep += 1;
  }

  startOver(): void {
    this.invoice = {
      createdOn: new Date(),
      customerId: 0,
      lineItems: [],
      updatedOn: new Date(),
    };
    this.invoiceStep = 1;
  }

  async initialize(): Promise<void> {
    await customerService
      .getCustomers()
      .then((res) => (this.customers = res))
      .catch(() => {});

    await inventoryService
      .getInventory()
      .then((res) => {
        this.inventory = res;
      })
      .catch(() => {});
  }

  addLineItem() {
    let newItem: ILineItem = {
      product: this.newItem.product,
      quantity: Number(this.newItem.quantity),
    };

    let existingItems = this.lineItems.map((item) => item?.product?.id);

    if (existingItems.includes(newItem?.product?.id)) {
      let lineItem = this.lineItems.find(
        (item) => item?.product?.id === newItem?.product?.id
      );

      if (lineItem) {
        let currentQuantity = Number(lineItem.quantity);
        let updatedQuantity = (currentQuantity += newItem.quantity);
        lineItem.quantity += updatedQuantity;
      }
    } else this.lineItems.push(this.newItem);

    // clear the newItem object
    this.newItem = {
      product: undefined,
      quantity: 0,
    };
  }

  async submitInvoice(): Promise<void> {
    this.invoice = {
      customerId: this.selectedCustomerId,
      lineItems: this.lineItems,
      createdOn: new Date(),
      updatedOn: new Date()
    };
    await invoiceService.makeNewInvoice(this.invoice);
  }

  finalizeOrder() {
    this.invoiceStep = 3;
  }

  async created(): Promise<void> {
    await this.initialize();
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";

/* deixa alinhados em uma row */
.invoice-steps-actions {
  display: flex;
  width: 100%;
}
.invoice-step-detail {
  margin: 1.2rem;
}

.invoice-order-list {
  margin-top: 1.2rem;
  padding: 0.8rem;

  .line-item {
    display: flex;
    border-bottom: 1px dashed #ccc;
    padding: 0.8rem;
  }

  .item-col {
    flex-grow: 1;
  }
}
.invoice-item-actions {
  display: flex;
}

.price-pre-tax {
  font-weight: bold;
}

.price-final {
  font-weight: bold;
  color: $solar-green;
}

.due {
  font-weight: bold;
}

.invoice-header {
  width: 100%;
  margin-bottom: 1.2rem;
}

.invoice-logo {
  padding-top: 1.4rem;
  text-align: center;

  img {
    width: 280px;
  }
}
</style>