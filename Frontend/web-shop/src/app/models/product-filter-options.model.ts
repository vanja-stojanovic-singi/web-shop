import { Brand } from "./brand.model";
import { Category } from "./category.model";

export interface ProductFilterOptions {
    brands: Brand[];
    categories: Category[];
}