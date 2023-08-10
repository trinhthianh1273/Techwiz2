import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { shareReplay } from 'rxjs';
import { IAppConfig } from 'src/app/appConfig/appconfig.interface';
import { APP_SERVICE_CONFIG } from 'src/app/appConfig/appconfig.service';
import { Product } from 'src/app/model/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClient:HttpClient,@Inject(APP_SERVICE_CONFIG)private config: IAppConfig ) {
    
  }
  getProduct$=this.httpClient.get<Product[]>(this.config.apiEndPoint).pipe(
    shareReplay(1)
  );
}
