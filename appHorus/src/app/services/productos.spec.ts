import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ProductsService } from './productos.service';

describe('AppComponent', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [RouterTestingModule],
    declarations: [ProductsService]
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(ProductsService);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

});
