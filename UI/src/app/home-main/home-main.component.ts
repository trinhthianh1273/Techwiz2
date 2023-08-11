import { Component } from '@angular/core';

@Component({
  selector: 'app-home-main',
  templateUrl: './home-main.component.html',
  styleUrls: ['./home-main.component.css']
})
export class HomeMainComponent {
  match:string='current match';
  matchSelected(match_param:string){
    this.match=match_param;
  }
}
