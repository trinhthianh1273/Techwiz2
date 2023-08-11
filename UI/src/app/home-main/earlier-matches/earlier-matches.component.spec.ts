import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EarlierMatchesComponent } from './earlier-matches.component';

describe('EarlierMatchesComponent', () => {
  let component: EarlierMatchesComponent;
  let fixture: ComponentFixture<EarlierMatchesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EarlierMatchesComponent]
    });
    fixture = TestBed.createComponent(EarlierMatchesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
