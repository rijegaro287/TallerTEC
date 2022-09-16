import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NuevoComponent2 } from './nuevo2.component';

describe('NuevoComponen2t', () => {
  let component: NuevoComponent2;
  let fixture: ComponentFixture<NuevoComponent2>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NuevoComponent2 ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NuevoComponent2);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
