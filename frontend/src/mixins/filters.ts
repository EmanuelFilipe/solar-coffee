import Vue from 'vue';
import moment from 'moment'

Vue.filter('price', function(number: number) {
  if (isNaN(number)) return '-';
  return `$ ${number.toFixed(2)}`;
});

Vue.filter('formatDate', function(date: Date) {
  return moment(date).format('MMMM Do YYYY');
});

export default {
  filters: {
    price: (value: number) => Vue.filter('price')!(value),
    formatDate: (value: Date) => Vue.filter('formatDate')!(value)
  }
};

