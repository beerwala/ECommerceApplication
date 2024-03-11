import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TwofaloginComponent } from './twofalogin.component';

describe('TwofaloginComponent', () => {
  let component: TwofaloginComponent;
  let fixture: ComponentFixture<TwofaloginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TwofaloginComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TwofaloginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
