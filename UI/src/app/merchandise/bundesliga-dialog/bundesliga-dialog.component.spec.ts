import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BundesligaDialogComponent } from './bundesliga-dialog.component';

describe('BundesligaDialogComponent', () => {
  let component: BundesligaDialogComponent;
  let fixture: ComponentFixture<BundesligaDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BundesligaDialogComponent]
    });
    fixture = TestBed.createComponent(BundesligaDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
