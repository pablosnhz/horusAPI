import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardMainComponent } from './card-main.component';

describe('CardMainComponent', () => {
  let component: CardMainComponent;
  let fixture: ComponentFixture<CardMainComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CardMainComponent]
    });
    fixture = TestBed.createComponent(CardMainComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
