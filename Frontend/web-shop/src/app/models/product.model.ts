import { Brand } from "./brand.model";
import { Category } from "./category.model";
import { ProductItem } from "./product-item.model";
import { Rate } from "./rate.model";

export interface Product {
    id: number;
    name: string;
    description: string;
    category: Category;
    items: ProductItem[];
    brand: Brand;
    avgRating: number;
    price: number;
    imageUrl: string;
}