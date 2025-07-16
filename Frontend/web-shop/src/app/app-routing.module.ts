import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop/shop.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ProductComponent } from './product/product.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [{
  path: 'login',
  component: LoginComponent
}, {
  path: 'register',
  component: RegisterComponent
}, {
  path: 'shop',
  component: ShopComponent
}, {
  path: 'shop/products/:id',
  component: ProductComponent
}, {
  path: 'profile',
  component: ProfileComponent
}, {
  path: 'search',
  component: ShopComponent
}, {
  path: '',
  redirectTo: 'shop',
  pathMatch: 'full'
}];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
