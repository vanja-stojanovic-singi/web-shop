import { Component, OnDestroy, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CartItem } from '../models/cart.model';
import { ProductsService } from '../services/products.service';
import { AuthService } from '../services/auth.service';
import { Subject, take, takeUntil } from 'rxjs';
import { CartService } from '../services/cart.service';
import { OrderService } from '../services/order.service';
import { Order, OrderItem } from '../models/order.model';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit, OnDestroy {
    private unsubscribeAll: Subject<void> = new Subject();

    user: User;
    editMode: boolean;
    editForm: FormGroup;
    passwordForm: FormGroup;
    myOrders: Order[] | undefined;
    loadingOrders: boolean;

    constructor(private userService: UserService, private authService: AuthService,
                private productsService: ProductsService, private orderService: OrderService, private router: Router) {
        this.editForm = new FormGroup({
            name: new FormControl('', [Validators.required]),
            email: new FormControl({value:'', disabled: true}, [Validators.required, Validators.email]),
            address: new FormControl('', [Validators.required]),
            city: new FormControl('', [Validators.required]),
            zip: new FormControl('', [Validators.required]),
            phone: new FormControl('', [Validators.required]),
            birthday: new FormControl('', [Validators.required])
        });

        this.passwordForm = new FormGroup({
            oldPassword: new FormControl('', [Validators.required]),
            newPassword: new FormControl('', [Validators.required])
        });
    }

    ngOnInit(): void {
        if (!this.authService.loggedUser) {
            this.router.navigateByUrl('/shop');
            return;
        }

        this.userService.getUserById(this.authService.loggedUser.id).pipe(takeUntil(this.unsubscribeAll))
            .subscribe(user => {
                this.user = user;
            });
        
        this.loadMyOrders();
    }

    ngOnDestroy(): void {
        this.unsubscribeAll.next();
        this.unsubscribeAll.complete();
    }

    editProfile() {
        const data = {
            name: this.user.name,
            email: this.user.email,
            address: this.user.address,
            city: this.user.city,
            zip: this.user.zip,
            phone: this.user.phone,
            birthday: this.user.birthday
        };

        this.editForm.setValue(data);
        this.editMode = true;
    }

    saveProfile() {
        if (!this.editForm.valid) {
            this.editForm.markAllAsTouched();
            return;
        }

        const formData = this.editForm.getRawValue();
        const newUser = {...this.user, ...formData} as User;
        this.userService.updateUser(this.authService.loggedUser.id, newUser).pipe(takeUntil(this.unsubscribeAll))
            .subscribe(user => {
                this.user = user;
                this.editMode = false;
            });
    }

    savePassword() {
        if (!this.passwordForm.valid) {
            this.passwordForm.markAllAsTouched();
            return;
        }

        const formData = this.passwordForm.getRawValue();
        this.userService.updatePassword(this.authService.loggedUser.id, formData.oldPassword, formData.newPassword)
            .pipe(takeUntil(this.unsubscribeAll)).subscribe(() => {
                this.authService.logout();
            });
    }

    cancelEdit() {
        this.editMode = false;
    }

    rate(item: OrderItem, rate: number) {
        this.orderService.addRate(item.id, rate).pipe(take(1)).subscribe(() => {
            this.loadMyOrders();
        });
    }

    private loadMyOrders() {
        this.loadingOrders = true;
        this.orderService.getMyOrders().pipe(takeUntil(this.unsubscribeAll))
            .subscribe((orders: Order[]) => {
                this.myOrders = orders;
                this.loadingOrders = false;
            });
    }
}
