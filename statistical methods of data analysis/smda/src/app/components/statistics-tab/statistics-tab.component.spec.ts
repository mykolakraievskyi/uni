import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatisticsTabComponent } from './statistics-tab.component';

describe('StatisticsTabComponent', () => {
  let component: StatisticsTabComponent;
  let fixture: ComponentFixture<StatisticsTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StatisticsTabComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StatisticsTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
