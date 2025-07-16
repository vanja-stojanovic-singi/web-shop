import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from './services/user.service';
import { ProductsService } from './services/products.service';
import { CartItem } from './models/cart.model';
import { NavigationStart, Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { CartService } from './services/cart.service';
import { OrderService } from './services/order.service';
import { Order } from './models/order.model';
import { ChatService } from './services/chat.service';
import { ChatBotResponse, ChatMessage } from './models/chat-message.model';
import { catchError, take } from 'rxjs';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
    @ViewChild('cartDrawer') cartDrawer: any;
    @ViewChild('chatMessageEl') chatMessageEl: any;
    @ViewChild('chatBodyEl') chatBodyEl: any;

    showLoginMessage: boolean = false;
    isChatOpen: boolean = false;
    chatMessages: ChatMessage[] = [{
        sender: 'bot',
        message: 'Hi. How can I help you today?',
        sentMessageFlg: false
    }];

    constructor(private router: Router, private orderService: OrderService,
                public authService: AuthService, public cartService: CartService,
                public userService: UserService, public productService: ProductsService, private chatService: ChatService) { }

    ngOnInit(): void {
        this.router.events
            .subscribe((ev) => {
                if (ev instanceof NavigationStart) {
                    this.cartDrawer.close();
                }
            });
    }

    itemCountAdd(item: CartItem) {
        item.count++;
        this.cartService.increaseCartTotal(item.product.price);
    }

    itemCountRemove(item: CartItem, index: number) {
        item.count--;

        if (item.count == 0) {
            this.removeItem(index);
        }
        else {
            this.cartService.decreaseCartTotal(item.product.price);
        }
    }

    clearCart() {
        this.cartService.clearCart();
    }

    removeItem(index: number) {
        this.cartService.removeItemFromCart(index);
    }

    order() {
        this.showLoginMessage = false;

        if (this.authService.loggedUser) {
            const order = this.cartService.getCartOrder();

            console.log(order);
            this.orderService.createOrder(order).subscribe(() => {
                this.cartDrawer.close();
                this.cartService.clearCart();
                this.router.navigateByUrl('/profile');
            });
        }
        else {
            this.showLoginMessage = true;
        }
    }


    toggleChat() {
        this.isChatOpen = !this.isChatOpen;

        if (this.isChatOpen) {
            setTimeout(() => {
                this.chatMessageEl.nativeElement.focus();
            }, 400);
        }
    }

    keyDownEvent(event: KeyboardEvent) {
        if (event.key == 'Enter') {
            this.sendMessage();
        }
    }

    sendMessage() {
        const message = this.chatMessageEl.nativeElement.value;
        this.chatMessages.push({
            sender: 'user',
            message: message,
            sentMessageFlg: true
        });

        this.chatMessageEl.nativeElement.value = '';
        this.chatMessageEl.nativeElement.focus();
        this.scrollMessages();

        this.chatService.sendMessage(message).pipe(take(1), catchError((err, caught) => {
            console.log(err);

            return caught;
        })).subscribe((resp: ChatBotResponse[]) => {
            if (resp && resp.length > 0) {
                this.chatMessages.push({
                    sender: 'bot',
                    message: resp[0].text,
                    sentMessageFlg: false
                });
                
                this.scrollMessages();
            }
        });
    }

    private scrollMessages() {
        setTimeout(() => {
            this.chatBodyEl.nativeElement.scrollTop = this.chatBodyEl.nativeElement.scrollHeight;
        }, 50);
    }
}
