import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSidenav } from '@angular/material/sidenav';
import { Router } from '@angular/router';
import { BreakpointObserver } from '@angular/cdk/layout';
@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent {
  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;
  constructor(
    private observer: BreakpointObserver,
    private dialog: MatDialog,
    private router: Router) {

  }
  lessthan: boolean = false;
  ngAfterViewInit() {
    this.observer.observe(['(max-width: 870px)'])
      .subscribe((res) => {
        if (res.matches) {
          this.lessthan = true;
        }
        else {
          this.lessthan = false;
          this.sidenav.close();
        }
      });
  }
  // openDialog() {
  //   this.dialog.open(GetTrainComponent, {
  //     height: '50%',
  //     width: '75%'
  //   });
  // }
  gotoTrainDetails() {
    this.router.navigate(['']);
  }
}
