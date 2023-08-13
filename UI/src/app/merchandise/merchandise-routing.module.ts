import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShoppingComponent } from './shopping/shopping.component';
import { ShowProductComponent } from './show-product/show-product.component';

const routes: Routes = [
  {
    path: 'shopping', component: ShoppingComponent
  },{path:'showproduct/:productid',component: ShowProductComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MerchandiseRoutingModule { }