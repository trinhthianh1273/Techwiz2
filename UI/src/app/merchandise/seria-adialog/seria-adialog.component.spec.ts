import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SeriaADialogComponent } from './seria-adialog.component';

describe('SeriaADialogComponent', () => {
  let component: SeriaADialogComponent;
  let fixture: ComponentFixture<SeriaADialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SeriaADialogComponent]
    });
    fixture = TestBed.createComponent(SeriaADialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
