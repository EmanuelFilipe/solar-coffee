import { BaseService } from './base-service'
import { IServiceResponse } from '../types/ServiceResponse'
import { ICustomer } from '../types/Customer'

/**
 * Customer Service
 * Provides UI business logic associated with Customers in our system
 */
export class CustomerService extends BaseService {

    public async getCustomers(): Promise<ICustomer[]> {
        try {
          let result: any = await this.http.get('/customer/');
          return result.data;
        } catch (error) {
          console.error('Error fetching customers:', error);
          throw error;
        }
    }

    public async addCustomers(newCustomer: ICustomer): Promise<IServiceResponse<ICustomer>> {
        try {
          let result: any = await this.http.post('/customer/', newCustomer);
          return result.data;
        } catch (error) {
          console.error('Error fetching customers:', error);
          throw error;
        }
    }

    public async deleteCustomer(customerId: number): Promise<boolean> {
        try {
          let result: any = await this.http.delete(`/customer/${customerId}`);
          return result.data;
        } catch (error) {
          console.error('Error fetching customers:', error);
          throw error;
        }
    }
}