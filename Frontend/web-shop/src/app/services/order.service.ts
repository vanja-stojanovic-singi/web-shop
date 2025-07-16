import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from './auth.service';
import { Order } from '../models/order.model';

@Injectable({
    providedIn: 'root'
})
export class OrderService {
    private baseUrl: string = 'https://localhost:7208/api';

    constructor(private http: HttpClient, private authService: AuthService) {
    }

    createOrder(order: Order) {
        return this.http.post<Order>(`${this.baseUrl}/orders`, order, {headers: this.authService.getAuthHeaders()});
    }

    getMyOrders() {
        return this.http.get<Order[]>(`${this.baseUrl}/orders/my`, {headers: this.authService.getAuthHeaders()});
    }

    addRate(orderItemId: number, rate: number) {
        return this.http.post<void>(`${this.baseUrl}/orders/orderitem/${orderItemId}/rate`, rate,
            {headers: this.authService.getAuthHeaders()});
    }
}
