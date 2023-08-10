import { CurrencyPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Greaterthan0Validator } from 'src/app/validators/greaterthan0-validator';
import { MatRadioChange } from '@angular/material/radio';
@Component({
  selector: 'app-shopping',
  templateUrl: './shopping.component.html',
  styleUrls: ['./shopping.component.css']
})
export class ShoppingComponent implements OnInit {
  priceForm!: FormGroup;
  priceFrom:number=0;
  priceTo:number=0;
  constructor(private fb:FormBuilder,private currency:CurrencyPipe){

  }
  ngOnInit(): void {
    this.priceForm=this.fb.group({
      priceTo:['',Greaterthan0Validator.ValidateName ],
      priceFrom: ['',Greaterthan0Validator.ValidateName]
    });
    // this.priceForm.get('priceFrom')?.valueChanges.subscribe(
    //   form=>{
    //     console.log(form);
    //     this.priceForm.patchValue({
    //       priceFrom:this.currency.transform(this.priceForm.get('priceFrom')?.value),
    //     })
    //   }
    // )
    
  }
  kitTypeChange(event:MatRadioChange){
    console.log(event.value);
  }
  choosePrice(){
    if (this.priceForm.get('priceFrom')?.value<this.priceForm.get('priceTo')?.value){
      this.priceFrom=this.priceForm.get('priceFrom')?.value;
      this.priceTo=this.priceForm.get('priceTo')?.value;
    }
    else if(this.priceForm.get('priceFrom')?.value!=null&&this.priceForm.get('priceTo')?.value==null){
      this.priceFrom=this.priceForm.get('priceFrom')?.value;
      this.priceTo=9999999999;
    }
    else if(this.priceForm.get('priceFrom')?.value==null&&this.priceForm.get('priceTo')?.value!=null){
      this.priceFrom=0;
      this.priceTo=this.priceForm.get('priceTo')?.value;
    }
    else if(this.priceForm.get('priceFrom')?.value>this.priceForm.get('priceTo')?.value&&this.priceForm.get('priceFrom')?.value!=null
    &&this.priceForm.get('priceTo')?.value!=null){
      this.priceForm.patchValue({
        priceFrom:this.priceForm.get('priceTo')?.value,
        priceTo:this.priceForm.get('priceFrom')?.value
      })
      this.priceFrom=this.priceForm.get('priceTo')?.value;
      this.priceTo=this.priceForm.get('priceFrom')?.value;
    }
    console.log(this.priceFrom);
    console.log(this.priceTo);
    console.log("hello");
  }
  panelOpenState = false;
}
