import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MerchandiseComponent } from './merchandise.component';

describe('MerchandiseComponent', () => {
  let component: MerchandiseComponent;
  let fixture: ComponentFixture<MerchandiseComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MerchandiseComponent]
    });
    fixture = TestBed.createComponent(MerchandiseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
