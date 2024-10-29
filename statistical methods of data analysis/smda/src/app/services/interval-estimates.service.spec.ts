import { TestBed } from '@angular/core/testing';

import { IntervalEstimatesService } from './interval-estimates.service';

describe('IntervalEstimatesService', () => {
  let service: IntervalEstimatesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IntervalEstimatesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
