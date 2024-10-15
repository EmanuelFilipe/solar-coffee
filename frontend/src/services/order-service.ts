import { BaseService } from './base-service'

export class OrderService extends BaseService { 

    /**
     * Order Service
     * Provides UI business logic associated with sales orders
     */
    public async getOrders(): Promise<any> {
        try {
            let result: any = await this.http.get('/order/');
            return result.data;
          } catch (error) {
            console.error('Error fetching orders:', error);
            throw error;
          }
    }

    public async markOrderComplete(id: number): Promise<any> {
        try {
            let result: any = await this.http.patch(`/order/complete/${id}`);
            return result.data;
          } catch (error) {
            console.error('Error fetching order complete:', error);
            throw error;
          }
    }

}