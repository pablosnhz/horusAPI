import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardSecondaryComponent } from './card-secondary.component';

describe('CardSecondaryComponent', () => {
  let component: CardSecondaryComponent;
  let fixture: ComponentFixture<CardSecondaryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CardSecondaryComponent]
    });
    fixture = TestBed.createComponent(CardSecondaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
