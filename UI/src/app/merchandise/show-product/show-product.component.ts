import { Component, OnInit } from '@angular/core';
import { ImageTransferService } from 'src/app/shared/product/image-transfer.service';
import { ProductService } from 'src/app/shared/product/product.service';

@Component({
  selector: 'app-show-product',
  templateUrl: './show-product.component.html',
  styleUrls: ['./show-product.component.css']
})
export class ShowProductComponent implements OnInit {
  fetchedPictureUrl: string='';
  constructor(private imgTransfer: ImageTransferService){
  }
  ngOnInit(): void {
    this.fetchedPictureUrl='./../assets/img/'+this.imgTransfer.getProductImg();
  }
  
}
