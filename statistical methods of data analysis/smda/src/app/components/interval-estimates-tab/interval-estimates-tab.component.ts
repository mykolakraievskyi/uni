import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IntervalEstimatesService } from '../../services/interval-estimates.service';

@Component({
  selector: 'smda-interval-estimates-tab',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './interval-estimates-tab.component.html',
  styleUrl: './interval-estimates-tab.component.css',
})
export class IntervalEstimatesTabComponent {
  data: number[] = [
    0.38, 0.37, 0.38, 0.36, 0.4, 0.36, 0.48, 0.14, 0.28, 0.31, 0.57, 0.65, 0.78,
    0.7, 0.52, 0.19, 0.11, 0.0, 0.57, 0.42, 0.25, 0.25, 0.32, 0.36, 0.44, 0.65,
    0.88, 0.57,
  ];
  alphaValues = [0.05, 0.01, 0.001];
  selectedAlpha = 0.05;
  mean: number = 0;
  gamma: number = 0;
  variance: number = 0;
  std: number = 0;
  intervalDV: [number, number] = [0, 0];
  intervalDN: [number, number] = [0, 0];
  intervalStd: [number, number] = [0, 0];

  get lap() {
    return this.intervalService.lap;
  }

  get q() {
    return this.intervalService.q;
  }

  get stu() {
    return this.intervalService.stu;
  }

  constructor(private intervalService: IntervalEstimatesService) {
    this.calculateResults();
  }

  calculateResults() {
    this.gamma = 1 - this.selectedAlpha;
    this.mean = this.intervalService.getMean(this.data);
    this.variance = this.intervalService.getCorrectedDispersion(this.data);
    this.std = this.intervalService.getCorrectedStd(this.data);

    this.intervalDV = this.intervalService.intervalMeanDV(
      this.data,
      this.gamma
    );
    this.intervalDN = this.intervalService.intervalMeanDN(
      this.data,
      this.gamma
    );
    this.intervalStd = this.intervalService.intervalMeanStd(
      this.data,
      this.gamma
    );
  }

  stringify(object: object) {
    return JSON.stringify(object);
  }
}
