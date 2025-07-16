import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../models/product.model';
import { ProductsService } from '../services/products.service';
import { MatChipListboxChange } from '@angular/material/chips';
import { Subject, take, takeUntil } from 'rxjs';
import { CartService } from '../services/cart.service';
import { ProductRating } from '../models/product-rating.model';
import { ProductItem } from '../models/product-item.model';

@Component({
    selector: 'app-product',
    templateUrl: './product.component.html',
    styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit, OnDestroy {
    private unsubscribeAll: Subject<void> = new Subject();

    loading: boolean;
    productId: number;
    product: Product;
    productRatings: ProductRating[];
    selectedSize: string = '';
    showErrorMessage: boolean = false;
    showSuccess: boolean = false;

    constructor(private productsService: ProductsService, private cartService: CartService,
        private route: ActivatedRoute, private router: Router) {}

    ngOnInit(): void {
        this.loading = true;
        this.route.params.subscribe(params => {
            this.productId = +params['id'];

            this.productsService.getProductById(this.productId).pipe(takeUntil(this.unsubscribeAll))
                .subscribe(product => {
                    if (!product) {
                        this.router.navigateByUrl('/shop');
                        return;
                    }

                    this.product = product;
                    this.loading = false;
                });

            this.productsService.getProductRatings(this.productId).pipe(take(1)).subscribe((ratings: ProductRating[]) => {
                this.productRatings = ratings;
            });
        });
    }

    ngOnDestroy(): void {
        this.unsubscribeAll.next();
        this.unsubscribeAll.complete();
    }

    addToCart() {
        this.showErrorMessage = false;

        if (!this.selectedSize) {
            this.showErrorMessage = true;
            return;
        }

        this.cartService.addToCart(<Product>this.product, this.selectedSize);
        this.showAlert();
    }

    showAlert() {
        this.showSuccess = true;
        setTimeout(() => {
            this.showSuccess = false;
        }, 5000);
    }

    changeSize(ev: MatChipListboxChange) {
        this.selectedSize = ev.value || '';
    }

    getDistinctSizes(array: ProductItem[]) {
        return [... new Set(array.map(a => a.size))];
    }
}