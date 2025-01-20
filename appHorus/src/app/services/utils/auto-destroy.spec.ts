import { AutoDestroyService } from 'src/app/services/utils/auto-destroy.service';
import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

describe('AppComponent', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [RouterTestingModule],
    declarations: [AutoDestroyService]
  }));

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AutoDestroyService);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

});
