import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from 'src/app/model/product';
import { ImageTransferService } from 'src/app/shared/product/image-transfer.service';
import { ProductService } from 'src/app/shared/product/product.service';

@Component({
  selector: 'app-product-shopping',
  templateUrl: './product-shopping.component.html',
  styleUrls: ['./product-shopping.component.css']
})
export class ProductShoppingComponent implements OnInit {
  // productID:string;
  //   productName:string;
  //   categoryName:string;
  //   price:number;
  //   playerName:string;
  //   teamName:string;
  //   pictureUrl:string;
  //   description:string;
  products: Product[] = [{
    productID:"1",
    productName:"ManCity-home",
    categoryName:"shirt",
    price:50,
    playerName:"",
    teamName:"Man City",
    pictureUrl:"\\premier_league\\Mancity\\shirt\\home.png",
    description:"Manchester City Puma Home Shirt 2023-24 - Kids with Haaland 9 printing"
  },
  {
    productID:"2",
    productName:"ManCity-home",
    categoryName:"shirt",
    price:50,
    playerName:"",
    teamName:"Man City",
    pictureUrl:"\\premier_league\\Mancity\\shirt\\home.png",
    description:"Manchester City Puma Home Shirt 2023-24 - Kids with Haaland 9 printing"
  },
  {
    productID:"3",
    productName:"ManCity-home",
    categoryName:"shirt",
    price:50,
    playerName:"",
    teamName:"Man City",
    pictureUrl:"\\premier_league\\Mancity\\shirt\\home.png",
    description:"Manchester City Puma Home Shirt 2023-24 - Kids with Haaland 9 printing"
  },
  {
    productID:"4",
    productName:"ManCity-home",
    categoryName:"shorts",
    price:30,
    playerName:"",
    teamName:"Man City",
    pictureUrl:"\\premier_league\\Mancity\\shorts\\home.png",
    description:"Manchester City Puma Home Shorts 2023-24 - Kids"
  },
  {
    productID:"5",
    productName:"ManCity-home",
    categoryName:"shorts",
    price:20,
    playerName:"",
    teamName:"Man City",
    pictureUrl:"\\premier_league\\Mancity\\shorts\\home.png",
    description:"Manchester City Puma Home Shorts 2023-24 - Kids"
  },
  {
    productID:"6",
    productName:"ManCity-home",
    categoryName:"shorts",
    price:50,
    playerName:"",
    teamName:"Man City",
    pictureUrl:"\\premier_league\\Mancity\\shorts\\home.png",
    description:"Manchester City Puma Home Shorts 2023-24 - Kids"
  },
  {
    productID:"8",
    productName:"ManCity-home",
    categoryName:"shirt",
    price:10,
    playerName:"",
    teamName:"Man City",
    pictureUrl:"\\premier_league\\Mancity\\shirt\\home.png",
    description:"Manchester City Puma Home Shirt 2023-24 - Kids with Haaland 9 printing"
  },
  {
    productID:"8",
    productName:"ManCity-home",
    categoryName:"shirt",
    price:10,
    playerName:"",
    teamName:"Man City",
    pictureUrl:"\\premier_league\\Mancity\\shirt\\home.png",
    description:"Manchester City Puma Home Shirt 2023-24 - Kids with Haaland 9 printing"
  }
];
  noRowProducts:number[]=[];
  constructor(private router:Router,private imgTransfer: ImageTransferService){

  }
ngOnInit(): void {
  for(let i=0;i<this.products.length/3;i++) {
    this.noRowProducts.push(i*3);
  }
  console.log("hello world0");
  console.log(this.noRowProducts);
}
concatUrlImage(product: Product){
  return  ".\\..\\assets\\img"+ product.pictureUrl;
}
buyProduct(product: Product){
  this.imgTransfer.setProductImg(product.pictureUrl);
  this.router.navigate(['showproduct/',product.productID]);
}

}
