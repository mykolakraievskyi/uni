import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class StatisticsService {
  getAverage(data: number[]): number {
    return data.reduce((acc, val) => acc + val, 0) / data.length;
  }

  getDispersion(data: number[]): number {
    const avg = this.getAverage(data);
    return (
      data.reduce((acc, val) => acc + Math.pow(val - avg, 2), 0) / data.length
    );
  }

  getMedian(data: number[]): number {
    const sorted = [...data].sort((a, b) => a - b);
    const mid = Math.floor(sorted.length / 2);
    return sorted.length % 2 !== 0
      ? sorted[mid]
      : (sorted[mid - 1] + sorted[mid]) / 2;
  }

  getMode(data: number[]): number[] | null {
    const counts: { [key: number]: number } = {};
    data.forEach((num) => (counts[num] = (counts[num] || 0) + 1));
    const maxCount = Math.max(...Object.values(counts));
    const modes = Object.keys(counts)
      .filter((key) => counts[+key] === maxCount)
      .map(Number);
    return modes.length === data.length ? null : modes;
  }

  getSTD(data: number[]): number {
    return Math.sqrt(this.getDispersion(data));
  }

  getRange(data: number[]): number {
    return Math.max(...data) - Math.min(...data);
  }

  getCorrectedDispersion(data: number[]): number {
    const n = data.length;
    return (this.getDispersion(data) * n) / (n - 1);
  }

  getCorrectedSTD(data: number[]): number {
    return Math.sqrt(this.getCorrectedDispersion(data));
  }

  getVariation(data: number[]): number {
    return this.getCorrectedSTD(data) / this.getAverage(data);
  }

  getPM(data: number[], order: number): number {
    return (
      data.reduce((acc, val) => acc + Math.pow(val, order), 0) / data.length
    );
  }

  getCM(data: number[], order: number): number {
    const avg = this.getAverage(data);
    return (
      data.reduce((acc, val) => acc + Math.pow(val - avg, order), 0) /
      data.length
    );
  }

  getAsymmetry(data: number[]): number {
    return this.getCM(data, 3) / Math.pow(this.getSTD(data), 3);
  }

  getExcess(data: number[]): number {
    return this.getCM(data, 4) / Math.pow(this.getSTD(data), 4) - 3;
  }
}
