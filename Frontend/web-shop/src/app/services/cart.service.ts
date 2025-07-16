import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Cart, CartItem } from '../models/cart.model';
import { Product } from '../models/product.model';
import { AuthService } from './auth.service';
import { Order, OrderItem } from '../models/order.model';
import { ProductItem } from '../models/product-item.model';
import { User } from '../models/user.model';

@Injectable({
    providedIn: 'root'
})
export class CartService {
    cart: Cart;

    constructor(private authService: AuthService) {
        this.cart = new Cart();
    }

    addToCart(product: Product, size: string) {
        const item = {
            product: product,
            size: size,
            count: 1
        } as CartItem;

        this.cart.items = [...this.cart.items, item];
        this.cart.total += item.product.price;
    }

    clearCart() {
        this.cart = new Cart();
    }

    removeItemFromCart(index: number) {
        const deletedItems = this.cart.items.splice(index, 1);

        if (deletedItems[0].count == 0) {
            this.cart.total -= deletedItems[0].product.price;
        }
        else {
            this.cart.total -= deletedItems[0].count * deletedItems[0].product.price;
        }
    }

    removeItemFromMyCart(index: number) {
        const orderedCarts = <Cart[]>JSON.parse(localStorage.getItem('ordered_carts') || '[]');

        const myCartIndex = orderedCarts.findIndex(c => c.userId == this.authService.loggedUser?.id);

        const removedItem = orderedCarts[myCartIndex].items.splice(index, 1);
        orderedCarts[myCartIndex].total -= removedItem[0].product.price;

        localStorage.setItem('ordered_carts', JSON.stringify(orderedCarts));
    }

    // saveCart() {
    //     const userId = this.authService.loggedUser?.id;

    //     const orderedCarts = <Cart[]>JSON.parse(localStorage.getItem('ordered_carts') || '[]');

    //     const myCartIndex = orderedCarts.findIndex(c => c.userId == userId);

    //     if (myCartIndex > -1) {
    //         orderedCarts[myCartIndex].items = [...orderedCarts[myCartIndex].items, ...this.cart.items];
    //         orderedCarts[myCartIndex].total += this.cart.total;
    //     }
    //     else {
    //         const cart = this.cart;
    //         cart.userId = userId;
    //         orderedCarts.push(cart);
    //     }

    //     localStorage.setItem('ordered_carts', JSON.stringify(orderedCarts));
    //     this.cart = new Cart();
    // }

    getCartOrder() {
        // const cart = this.myCart();

        const customer = new User();
        customer.id = this.authService.loggedUser.id;
        const order = {
            customer: customer,
            items: []
        } as Order;

        this.cart.items.forEach(item => {
            for (let i = 0; i < item.count; i++) {
                order.items.push({
                    id: 0,
                    price: item.product.price,
                    productId: item.product.id,
                    size: item.size,
                    product: item.product
                } as OrderItem);
            }
        });

        return order;
    }

    // updateCartItemStatus(itemIndex: number, status: string) {
    //     const orderedCarts = <Cart[]>JSON.parse(localStorage.getItem('ordered_carts') || '[]');
    //     const ind = orderedCarts.findIndex(c => c.userId == this.authService.loggedUser?.id);

    //     orderedCarts[ind].items[itemIndex].status = status;
    //     localStorage.setItem('ordered_carts', JSON.stringify(orderedCarts));
    // }

    increaseCartTotal(value: number) {
        this.cart.total += value;
    }

    decreaseCartTotal(value: number) {
        this.cart.total -= value;
    }
}
