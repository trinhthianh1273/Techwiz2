import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BundesligaDialogComponent } from '../bundesliga-dialog/bundesliga-dialog.component';
import { LaligaDialogComponent } from '../laliga-dialog/laliga-dialog.component';
import { Ligue1DialogComponent } from '../ligue1-dialog/ligue1-dialog.component';
import { PremierLeagueDialogComponent } from '../premier-league-dialog/premier-league-dialog.component';
import { SeriaADialogComponent } from '../seria-adialog/seria-adialog.component';

@Component({
  selector: 'app-competition',
  templateUrl: './competition.component.html',
  styleUrls: ['./competition.component.css']
})
export class CompetitionComponent {
  constructor(private dialog: MatDialog){

  }
  bundesligaButton(){
    this.dialog.open(BundesligaDialogComponent, {
      height: '30%',
      width: '40%'
    });
  }
  laligaButton(){
    this.dialog.open(LaligaDialogComponent, {
      height: '30%',
      width: '40%'
    });
  }
  ligue1Button(){
    this.dialog.open(Ligue1DialogComponent, {
      height: '30%',
      width: '40%'
    });
  }
  premierLeagueButton(){
    this.dialog.open(PremierLeagueDialogComponent, {
      height: '50%',
      width: '64.5%'
    });
  }
  seriaAButton(){
    this.dialog.open(SeriaADialogComponent, {
      height: '30%',
      width: '40%'
    });
  }
}
