import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardComponent3 } from './dashboard3.component';

describe('DashboardComponent3', () => {
  let component: DashboardComponent3;
  let fixture: ComponentFixture<DashboardComponent3>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DashboardComponent3 ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashboardComponent3);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
