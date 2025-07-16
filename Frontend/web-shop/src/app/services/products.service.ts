import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import { AuthService } from './auth.service';
import { HttpClient } from '@angular/common/http';
import { ProductFilterOptions } from '../models/product-filter-options.model';
import { ProductRating } from '../models/product-rating.model';

@Injectable({
    providedIn: 'root'
})
export class ProductsService {
    private baseUrl: string = 'https://localhost:7208/api';

    constructor(private authService: AuthService, private http: HttpClient) {
        this.initData();
    }

    getProducts(search: string, brandIds: number[], categoryIds: number[], sizes: string[], categoryNames: string[]) {
        let url = `${this.baseUrl}/products?`;

        if (search) {
            url += `search=${search}&`;
        }

        if (brandIds && brandIds.length > 0) {
            brandIds.forEach(brandId => {
                url += `brandIds=${brandId}&`;
            });
        }

        if (categoryIds && categoryIds.length > 0) {
            categoryIds.forEach(categoryId => {
                url += `categoryIds=${categoryId}&`;
            });
        }

        if (sizes && sizes.length > 0) {
            sizes.forEach(size => {
                url += `sizes=${size}&`;
            });
        }

        if (categoryNames && categoryNames.length > 0) {
            categoryNames.forEach(cn => {
                url += `categoryNames=${cn}&`;
            });
        }

        if (url.endsWith('?') || url.endsWith('&')) {
            url = url.slice(0, url.length - 1);
        }

        return this.http.get<Product[]>(url);
    }

    getProductFilters() {
        return this.http.get<ProductFilterOptions>(`${this.baseUrl}/products/filters`);
    }

    getProductById(id: number) {
        return this.http.get<Product>(`${this.baseUrl}/products/${id}`);
    }

    getProductRatings(id: number) {
        return this.http.get<ProductRating[]>(`${this.baseUrl}/products/${id}/ratings`);
    }
    
    getSizes() {
        return ['XS', 'S', 'M', 'L', 'XL', 'XXL', 'XXXL'];
    }

    // getProductAvgRate(productId: number) {
    //     const ratesData = localStorage.getItem('product_rates');
    //     let rates: ProductRate[] = [];

    //     if (ratesData) {
    //         rates = <ProductRate[]>JSON.parse(ratesData);
            
    //         let sum = 0;
    //         const productRates = rates.filter(r => r.productId == productId);
    //         productRates.forEach(p => {
    //             sum += p.rate;
    //         });

    //         return sum / productRates.length;
    //     }

    //     return 0;
    // }

    initData() {
        const products = localStorage.getItem('shop_products');

        if (!products) {
            localStorage.setItem('shop_products', JSON.stringify([{
                id: 1,
                name: 'Jakna Guess Lucia',
                description: "Guess Lucia Bum Bag Puffa je ženska jakna sa fotografije. Ova crna ženska jakna je pufnasta, topla i lako se kombinuje uz zimske odevne kombinacije. Sa prednje strane se nalazi cibzar, kao i pojas sa ušivenom torbicom za osnovne stvari u njoj. Oko vrata se nalazi kragna, a na ivicama rukava suženje.",
                brand: 'Guess',
                manufacturingDate: new Date('2022-04-12'),
                price: 14000,
                sizes: ['XXL'],
                type: 'Jakna',
                imageUrl: 'https://www.n-sport.net/UserFiles/products/big/09/29/zenska-jakna-guess-lucia-bum-bag-puffa-W3BL19WEX12-JBL.jpg'
            }, {
                id: 2,
                name: 'Tommy Hooded Down jakna',
                description: "Tommy Hilfiger Basic Hooded Down Jacket je ženska jakna sa fotografije. Ova crna zimska jakna je pufnasta, ima dva džepa sa prednje strane, cibzar, kragnu i kapuljaču sa cicom. Na sredini grudi se nalazi znak brenda Tommy Hilfiger.",
                brand: 'Tommy Hilfiger',
                manufacturingDate: new Date('2022-10-10'),
                price: 13400,
                sizes: ['M', 'L', 'XXL'],
                type: 'Jakna',
                imageUrl: 'https://www.n-sport.net/UserFiles/products/big/11/08/zenska-zimska-jakna-tommy-hilfiger-basic-hooded-down-jacket-DW0DW08588-BDS.jpg'
            }, {
                id: 3,
                name: 'Zimska jakna Replay',
                description: "Replay ženska jakna je sjajne braon boje. Predeo ramena i kragnu krasi crni materijal izrade što upotpunjuje dizajn modela. Jakna se kopča na cibzar i ima dva standardna džepa.",
                brand: 'Replay',
                manufacturingDate: new Date('2023-11-03'),
                price: 17200,
                sizes: ['XS', 'M'],
                type: 'Jakna',
                imageUrl: 'https://www.n-sport.net/UserFiles/products/big/12/02/zenska-zimska-jakna-replay-771984464-826.jpg'
            }, {
                id: 4,
                name: 'CK Gradient duks',
                description: "Calvin Klein ženski duks Gradient Ck Hoodie je crne boje.Ovaj ženski duks ima kapuljaču sa učkurima. Na ivicama rukava nalaze se ranfle. Logo brenda nalazi se na sredini prednje strane duksa.",
                brand: 'Calvin Klein',
                manufacturingDate: new Date('2023-04-19'),
                price: 9300,
                sizes: ['M', 'L', 'XL'],
                type: 'Duks',
                imageUrl: 'https://www.n-sport.net/UserFiles/products/big/09/29/zenski-duks-calvin-klein-gradient-ck-hoodie-J20J222346-BEH.jpg'
            }, {
                id: 5,
                name: 'Replay duks sa kapuljacom',
                description: "Replay ženski duks sa fotografije je crne boje. Lagano pada preko tela, ima kapuljaču i ima ranfle na ivicama kod pasa i rukava. Sa prednje strane se nalazi potpis brenda Replay i tekst Rose Label, a sa zadnje strane je beli pravougaonik sa tekstom.",
                brand: 'Replay',
                manufacturingDate: new Date('2022-08-09'),
                price: 10900,
                sizes: ['S', 'XL', 'XXL'],
                type: 'Duks',
                imageUrl: 'https://www.n-sport.net/UserFiles/products/big/01/23/zenski-duks-replay-3704C23358P-098.jpg'
            }, {
                id: 6,
                name: 'Liu Jo Felpa Aperta',
                description: "Liu Jo Felpa Aperta Sweatshirt je ženski duks sa fotografije. Ovaj duks ima bež rukave i grudi, kao i kapuljaču, dok su svi ostali delovi bele boje. Čak su i ivice rukava bele boje. Sa prednje strane su dva džepa i cibzar celom dužinom.",
                brand: 'Liu Jo',
                manufacturingDate: new Date('2022-04-02'),
                price: 5700,
                sizes: ['XS', 'XXL'],
                type: 'Duks',
                imageUrl: 'https://www.n-sport.net/UserFiles/products/big/08/24/zenski-duks-liu-jo-felpa-aperta-sweatshirt-TF3073FS576-A72.jpg'
            }, {
                id: 7,
                name: 'Farmerke Izzie Hr Ankl',
                description: "Ženske farmerke dostupne su u klasičnoj denim boji. Farmerke su nešto šireg kroja i dubljeg profila. Zbog svog klasičnog izgleda lako se mogu kombinovati i koristiti u različitim prilikama.",
                brand: 'Tommy Hilfiger',
                manufacturingDate: new Date('2023-09-12'),
                price: 7500,
                sizes: ['XS', 'M', 'L', 'XXL'],
                type: 'Farmerke',
                imageUrl: 'https://www.n-sport.net/UserFiles/products/big/08/23/zenske-farmerke-tommy-hilfiger-izzie-hr-sl-ank-cg4139-DW0DW15993-1A5.jpg'
            }, {
                id: 8,
                name: 'Farmerke Replay',
                description: "Replay ženske farmerke izrađene su od pamuka i elastina što obezbeđuje udobnost čak i pri celodnevnom nošenju. Ženske farmerke su crne boje, prate liniju tela i blago su izbeljene u predelu butina.",
                brand: 'Replay',
                manufacturingDate: new Date('2021-05-20'),
                price: 6899,
                sizes: ['XS'],
                type: 'Farmerke',
                imageUrl: 'https://www.n-sport.net/UserFiles/products/big/01/23/zenske-farmerke-replay-68951A319-096.jpg'
            }, {
                id: 9,
                name: 'Guess Girly',
                description: "GIRLY relaxed fit farmerke sa dubokim strukom. Ravan kroj. Oko dzepova imaju cvetni dizajn.",
                brand: 'Guess',
                manufacturingDate: new Date('2021-12-03'),
                price: 3600,
                sizes: ['XXL'],
                type: 'Farmerke',
                imageUrl: 'https://www.n-sport.net/UserFiles/products/big/09/22/zenske-farmerke-guess-girly-W2YA16D4PW1-OBS.jpg'
            }, {
                id: 10,
                name: 'Guess Icon Tee',
                description: "Guess Ss Cn Icon Tee je majica kratkih rukava sa fotografije. Ova ženska majica je klasična Guess majica kratkih rukava sa originalnim i dobro poznatim znakom brenda sa prednje strane.",
                brand: 'Guess',
                manufacturingDate: new Date('2023-04-17'),
                price: 2899,
                sizes: ['S', 'M', 'L'],
                type: 'Majca',
                imageUrl: 'https://www.n-sport.net/UserFiles/products/big/11/08/zenska-majica-guess-ss-cn-icon-tee-W3BI42I3Z14-G01.jpg'
            }, {
                id: 11,
                name: 'Liu Jo T-Shirt',
                description: "Liu Jo T-Shirt je majica kratkih rukava sa fotografije. Ova bela majica ima okrugli izrez oko vrata, strukirana je i dugačka do pasa. Na sredini grudi nalazi se grafika malog braon psa, koji posmatra umilnim očima.",
                brand: 'Liu Jo',
                manufacturingDate: new Date('2023-06-02'),
                price: 3099,
                sizes: ['XS', 'S', 'M', 'L'],
                type: 'Majca',
                imageUrl: 'https://www.n-sport.net/UserFiles/products/big/08/24/zenska-majica-liu-jo-t-shirt-MF3313J6410-Q99.jpg'
            }, {
                id: 12,
                name: 'Calvin Klein Motion Floral',
                description: "Liu Jo T-Shirt je majica kratkih rukava sa fotografije. Ova bela majica ima okrugli izrez oko vrata, strukirana je i dugačka do pasa. Na sredini grudi nalazi se grafika malog braon psa, koji posmatra umilnim očima.",
                brand: 'Calvin Klein',
                manufacturingDate: new Date('2023-07-22'),
                price: 4500,
                sizes: ['S', 'M', 'XL'],
                type: 'Majca',
                imageUrl: 'https://www.n-sport.net/UserFiles/products/big/07/04/zenska-majica-calvin-klein-motion-floral-aw-relaxed-tee-J20J220727-SCB.jpg'
            }]));
        }

        const productRates = localStorage.getItem('product_rates');

        if (!productRates) {
            localStorage.setItem('product_rates', JSON.stringify([{
                rate: 3,
                productId: 1,
                userId: 0
            }, {
                rate: 4,
                productId: 1,
                userId: 0
            }, {
                rate: 1,
                productId: 2,
                userId: 0
            }, {
                rate: 2,
                productId: 2,
                userId: 0
            }, {
                rate: 4,
                productId: 3,
                userId: 0
            }, {
                rate: 1,
                productId: 3,
                userId: 0
            }, {
                rate: 2,
                productId: 4,
                userId: 0
            }, {
                rate: 1,
                productId: 5,
                userId: 0
            }, {
                rate: 5,
                productId: 6,
                userId: 0
            }, {
                rate: 1,
                productId: 6,
                userId: 0
            }, {
                rate: 2,
                productId: 7,
                userId: 0
            }, {
                rate: 4,
                productId: 8,
                userId: 0
            }, {
                rate: 3,
                productId: 9,
                userId: 0
            }, {
                rate: 4,
                productId: 10,
                userId: 0
            }, {
                rate: 2,
                productId: 11,
                userId: 0
            }, {
                rate: 5,
                productId: 12,
                userId: 0
            }]));
        }
    }
}
