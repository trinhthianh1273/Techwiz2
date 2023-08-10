import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MerchandiseComponent } from './merchandise.component';
import { MerchandiseRoutingModule } from './merchandise-routing.module';
import { ProductShoppingComponent } from './product-shopping/product-shopping.component';
import { ShoppingComponent } from './shopping/shopping.component';
import { MaterialModule } from '../material/material.module';



@NgModule({
  declarations: [
    MerchandiseComponent,
    ProductShoppingComponent,
    ShoppingComponent
  ],
  imports: [
    CommonModule,
    MerchandiseRoutingModule,
    MaterialModule
  ]
})
export class MerchandiseModule { }
