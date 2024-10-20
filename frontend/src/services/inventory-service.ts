import { IProductInventory } from '@/types/Product';
import { IShipment } from '@/types/Shipment';
import { BaseService } from './base-service'
import { IInventoryTimeline } from '@/types/InventoryGraph';

/**
 * Inventory Service
 * Provides UI business logic associated with product inventory
 */
export class InventoryService extends BaseService {

    public async getInventory(): Promise<IProductInventory[]> {
      try {
        var result = await this.http.get('/inventory/');
        return result.data;
      } catch (error) {
        console.error('Error fetching inventory:', error);
        throw error;
      }
    }

    public async updateInventoryQuantity(shipment: IShipment) {
      try {
        let result = await this.http.patch('/inventory/', shipment)
        return result.data
      } catch (error) {
        console.error('Error updating inventory:', error);
        throw error;
      }
    }

    public async getSnapshotHistory(): Promise<IInventoryTimeline> {
      try {
        let result = await this.http.get('/inventory/snapshot')
        return result.data
      } catch (error) {
        console.error('Error getting snapshot history:', error);
        throw error;
      } 
    }
}