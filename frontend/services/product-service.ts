import { IProduct } from '../src/types/Product'
import { BaseService } from './base-service'

export class ProductService extends BaseService {

    public async archive(productId: number) {
        try {
            let result: any = await this.http.patch(`/product/${productId}`)
            return result.data
        } catch (error) {
            console.error('Error archiving product:', error);
            throw error;
        }
    }

    public async save(newProduct: IProduct) {
        try {
            let result: any = await this.http.post('/product/', newProduct)
            return result.data
        } catch (error) {
            console.error('Error saving new product:', error);
            throw error;
        }
    }
}