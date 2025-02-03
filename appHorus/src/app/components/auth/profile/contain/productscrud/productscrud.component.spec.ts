import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductscrudComponent } from './productscrud.component';

describe('ProductscrudComponent', () => {
  let component: ProductscrudComponent;
  let fixture: ComponentFixture<ProductscrudComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductscrudComponent]
    });
    fixture = TestBed.createComponent(ProductscrudComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
