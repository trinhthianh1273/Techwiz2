import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PremierLeagueDialogComponent } from './premier-league-dialog.component';

describe('PremierLeagueDialogComponent', () => {
  let component: PremierLeagueDialogComponent;
  let fixture: ComponentFixture<PremierLeagueDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PremierLeagueDialogComponent]
    });
    fixture = TestBed.createComponent(PremierLeagueDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
