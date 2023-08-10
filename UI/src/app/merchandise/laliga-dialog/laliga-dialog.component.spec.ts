import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LaligaDialogComponent } from './laliga-dialog.component';

describe('LaligaDialogComponent', () => {
  let component: LaligaDialogComponent;
  let fixture: ComponentFixture<LaligaDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LaligaDialogComponent]
    });
    fixture = TestBed.createComponent(LaligaDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
