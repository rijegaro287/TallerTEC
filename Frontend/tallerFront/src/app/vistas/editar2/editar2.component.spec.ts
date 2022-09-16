import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarComponent2 } from './editar2.component';

describe('EditarComponent2', () => {
  let component: EditarComponent2;
  let fixture: ComponentFixture<EditarComponent2>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditarComponent2 ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditarComponent2);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
