import { User } from "./user.model";

export interface Rate {
    id: number;
    rate: number;
    productId: number;
    customer: User;
}