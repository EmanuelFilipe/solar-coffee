import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Inventory from '@/views/Inventory.vue'
import Customers from '@/views/Customers.vue'
import CreateInvoice from '@/views/CreateInvoice.vue'
import Orders from '@/views/Orders.vue'
import Charts from '@/views/Charts.vue'

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'home',
    component: Inventory
  },
  {
    path: '/inventory',
    name: 'inventoty',
    component: Inventory
  },
  {
    path: '/customers',
    name: 'Customers',
    component: Customers
  },
  {
    path: '/invoice/new',
    name: 'create-invoice',
    component: CreateInvoice
  },
  {
    path: '/orders',
    name: 'Orders',
    component: Orders
  },
  {
    path: '/charts',
    name: 'Charts',
    component: Charts
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
