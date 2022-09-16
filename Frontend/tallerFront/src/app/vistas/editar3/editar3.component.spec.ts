import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditarComponent3 } from './editar3.component';

describe('EditarComponent3', () => {
  let component: EditarComponent3;
  let fixture: ComponentFixture<EditarComponent3>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditarComponent3 ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditarComponent3);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
