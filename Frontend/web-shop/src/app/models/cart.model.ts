import { Product } from "./product.model";

export class Cart {
    items: CartItem[];
    total: number;
    userId: string;

    constructor() {
        this.items = [];
        this.total = 0;
        this.userId = '';
    }
}

export class CartItem {
    product: Product;
    size: string;
    count: number;
    status: string;

    constructor() {
        this.product = {} as Product;
        this.size = '';
        this.count = 0;
        this.status = '';
    }
}