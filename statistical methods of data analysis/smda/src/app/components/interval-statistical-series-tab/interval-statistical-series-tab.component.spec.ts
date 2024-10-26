import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BaseChartDirective } from 'ng2-charts';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { IntervalStatisticalSeriesTabComponent } from './interval-statistical-series-tab.component';

describe('IntervalStatisticalSeriesTabComponent', () => {
  let component: IntervalStatisticalSeriesTabComponent;
  let fixture: ComponentFixture<IntervalStatisticalSeriesTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IntervalStatisticalSeriesTabComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(IntervalStatisticalSeriesTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
