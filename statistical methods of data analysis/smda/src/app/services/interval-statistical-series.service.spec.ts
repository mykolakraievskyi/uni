import { TestBed } from '@angular/core/testing';

import { IntervalStatisticalSeriesService } from './interval-statistical-series.service';

describe('IntervalStatisticalSeriesService', () => {
  let service: IntervalStatisticalSeriesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IntervalStatisticalSeriesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
