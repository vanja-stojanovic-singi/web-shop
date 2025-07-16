import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ProductsService } from '../services/products.service';
import { Product } from '../models/product.model';
import { MatSelectionListChange } from '@angular/material/list';
import { Subject, takeUntil } from 'rxjs';
import { Category } from '../models/category.model';
import { Brand } from '../models/brand.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-shop',
    templateUrl: './shop.component.html',
    styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit, OnDestroy {
    @ViewChild('searchInputElement') searchInputElement: any;

    private unsubscribeAll: Subject<void> = new Subject();

    loading: boolean = true;
    searchValue: string;

    products: Product[] = [];
    productBrands: Brand[] = [];
    productCategories: Category[] = [];
    productSizes: string[] = [];

    brandFilters: number[] = [];
    categoryFilters: number[] = [];
    categoryTypeFilters: string[] = [];
    sizeFilters: string[] = [];

    constructor(private productsService: ProductsService, private route: ActivatedRoute, private router: Router) {}

    ngOnInit(): void {
        this.loadFilters();

        console.log(this.router.url);
        if (this.router.url.startsWith('/search')) {
            this.route.queryParamMap.subscribe(params => {
                this.sizeFilters = [params.get('size')];
                this.categoryTypeFilters = [params.get('category')];
                this.loadProducts();
            });
        } else {
            this.loadProducts();
        }
    }

    ngOnDestroy(): void {
        this.unsubscribeAll.next();
        this.unsubscribeAll.complete();
    }

    loadProducts() {
        this.loading = true;
        this.productsService.getProducts(this.searchValue, this.brandFilters, this.categoryFilters, this.sizeFilters, this.categoryTypeFilters)
            .pipe(takeUntil(this.unsubscribeAll)).subscribe(products => {
                this.products = products;
                this.loading = false;
            });
    }

    loadFilters() {
        this.productsService.getProductFilters()
            .pipe(takeUntil(this.unsubscribeAll)).subscribe(productFilters => {
                this.productBrands = productFilters.brands;
                this.productCategories = productFilters.categories;
                this.productSizes = this.productsService.getSizes();
            });
    }

    filterChanged(ev: MatSelectionListChange, filter: string) {
        switch(filter) {
            case 'brand':
                this.brandFilters = ev.source.selectedOptions.selected.map(s => s.value);
                break;
            case 'category':
                this.categoryFilters = ev.source.selectedOptions.selected.map(s => s.value);
                break;
            case 'size':
                this.sizeFilters = ev.source.selectedOptions.selected.map(s => s.value);
                break;
            default:
                return;
        }

        this.loadProducts();
    }
}
