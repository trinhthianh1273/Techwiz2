import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageTransferService {
  private _productImg!:string;
  constructor() { }
  getProductImg(){
    return this._productImg;
  }
   setProductImg(value:string){
    this._productImg = value;
  }
}
