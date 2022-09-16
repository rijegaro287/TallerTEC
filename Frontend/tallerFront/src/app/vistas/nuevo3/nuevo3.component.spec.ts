import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NuevoComponent3 } from './nuevo3.component';

describe('NuevoComponent3', () => {
  let component: NuevoComponent3;
  let fixture: ComponentFixture<NuevoComponent3>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NuevoComponent3 ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NuevoComponent3);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
