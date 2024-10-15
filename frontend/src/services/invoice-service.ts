import { BaseService } from './base-service'
import { IInvoice } from '../types/Invoice'

export class InvoiceService extends BaseService {
    public async makeNewInvoice(invoice: IInvoice): Promise<boolean> {
        try {
            console.log('makeNewInvoice', invoice)
            let result: any = await this.http.post('/order/invoice/', invoice);
            return result.data;
        } catch (error) {
          console.error('Error fetching inventory:', error);
          throw error;
        }
      }
}