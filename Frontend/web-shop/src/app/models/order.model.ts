import { ProductItem } from "./product-item.model";
import { Product } from "./product.model";
import { User } from "./user.model";

export interface Order {
    id: number;
    createdDate: Date;
    customer: User;
    items: OrderItem[];
}

export interface OrderItem {
    id: number;
    price: number;
    size: string;
    product: Product;
}