import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IntervalEstimatesTabComponent } from './interval-estimates-tab.component';

describe('IntervalEstimatesTabComponent', () => {
  let component: IntervalEstimatesTabComponent;
  let fixture: ComponentFixture<IntervalEstimatesTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IntervalEstimatesTabComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IntervalEstimatesTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
