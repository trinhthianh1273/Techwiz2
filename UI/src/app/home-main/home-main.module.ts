import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CurrentMatchesComponent } from './current-matches/current-matches.component';
import { UpcomingMatchesComponent } from './upcoming-matches/upcoming-matches.component';
import { EarlierMatchesComponent } from './earlier-matches/earlier-matches.component';
import { MaterialModule } from '../material/material.module';


@NgModule({
  declarations: [
    CurrentMatchesComponent,
    UpcomingMatchesComponent,
    EarlierMatchesComponent,
    
  ],
  imports: [
    CommonModule,
    MaterialModule
  ],
  exports:[
    MaterialModule,
    CurrentMatchesComponent,
    UpcomingMatchesComponent,
    EarlierMatchesComponent,
  ]
})
export class HomeMainModule { }
