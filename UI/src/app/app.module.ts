import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { APP_CONFIG, APP_SERVICE_CONFIG } from './appConfig/appconfig.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavigationComponent } from './navigation/navigation.component';
import { MaterialModule } from './material/material.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CompetitionComponent } from './merchandise/competition/competition.component';
import { BundesligaDialogComponent } from './merchandise/bundesliga-dialog/bundesliga-dialog.component';
import { LaligaDialogComponent } from './merchandise/laliga-dialog/laliga-dialog.component';
import { Ligue1DialogComponent } from './merchandise/ligue1-dialog/ligue1-dialog.component';
import { PremierLeagueDialogComponent } from './merchandise/premier-league-dialog/premier-league-dialog.component';
import { SeriaADialogComponent } from './merchandise/seria-adialog/seria-adialog.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { MerchandiseModule } from './merchandise/merchandise.module';
import { CurrencyPipe } from '@angular/common';
@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    CompetitionComponent,
    BundesligaDialogComponent,
    LaligaDialogComponent,
    Ligue1DialogComponent,
    PremierLeagueDialogComponent,
    SeriaADialogComponent,
    NotFoundPageComponent
  ],
  imports: [
    BrowserModule,
    MerchandiseModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    NgbModule,
  ],
  providers: [
    {
      provide:APP_SERVICE_CONFIG,
      useValue:APP_CONFIG
    },
    CurrencyPipe

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
