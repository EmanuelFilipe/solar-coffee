import Vue from 'vue';

declare module 'vue/types/vue' {
  interface Vue {
    $filters: {
      price(value: number): string;
      formatDate(value: Date): string;
    };
  }
}
