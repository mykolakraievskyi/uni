import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class IntervalEstimatesService {
  public lap: { [key: string]: number } = {
    '0.95': 1.96,
    '0.99': 2.58,
    '0.999': 3.38,
  };
  public stu: { [key: string]: number } = {
    '0.95': 2.05,
    '0.99': 2.77,
    '0.999': 3.65,
  };
  public q: { [key: string]: number } = {
    '0.95': 0.22,
    '0.99': 0.32,
    '0.999': 0.46,
  };

  public getCorrectedDispersion(data: number[]): number {
    const mean = data.reduce((a, b) => a + b, 0) / data.length;
    const variance =
      data.reduce((acc, value) => acc + Math.pow(value - mean, 2), 0) /
      (data.length - 1);
    return variance;
  }

  public getCorrectedStd(data: number[]): number {
    return Math.sqrt(this.getCorrectedDispersion(data));
  }

  getMean(data: number[]): number {
    return data.reduce((a, b) => a + b, 0) / data.length;
  }

  intervalMeanDV(data: number[], gamma: number): [number, number] {
    const laplace = this.lap[gamma];
    const N = data.length;
    const mean = this.getMean(data);
    const std = Math.sqrt(this.getCorrectedDispersion(data));

    return [
      mean - (laplace * std) / Math.sqrt(N),
      mean + (laplace * std) / Math.sqrt(N),
    ];
  }

  intervalMeanDN(data: number[], gamma: number): [number, number] {
    const student = this.stu[gamma];
    const mean = this.getMean(data);
    const std = Math.sqrt(this.getCorrectedDispersion(data));

    return [
      mean - (student * std) / Math.sqrt(data.length),
      mean + (student * std) / Math.sqrt(data.length),
    ];
  }

  intervalMeanStd(data: number[], gamma: number): [number, number] {
    const std = this.getCorrectedStd(data);
    return [std * (1 - this.q[gamma]), std * (1 + this.q[gamma])];
  }
}
