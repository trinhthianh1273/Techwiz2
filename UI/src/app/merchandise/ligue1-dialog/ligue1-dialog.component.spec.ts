import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Ligue1DialogComponent } from './ligue1-dialog.component';

describe('Ligue1DialogComponent', () => {
  let component: Ligue1DialogComponent;
  let fixture: ComponentFixture<Ligue1DialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [Ligue1DialogComponent]
    });
    fixture = TestBed.createComponent(Ligue1DialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
