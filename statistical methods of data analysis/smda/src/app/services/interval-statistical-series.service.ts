import { Injectable, OnInit } from '@angular/core';
import { IntervalStatSeries } from '../models/interval-stat-series.model';

@Injectable({
  providedIn: 'root',
})
export class IntervalStatisticalSeriesService {
  constructor() {}

  public getIntervalsCount(data: number[]): number {
    return Math.round(1 + 3.322 * Math.log10(data.length));
  }

  public getRange(data: number[]): number {
    return Math.max(...data) - Math.min(...data);
  }

  public getIntervalStatSeries(data: number[]): IntervalStatSeries[] {
    const result: Array<{
      min: number;
      max: number;
      avg: number;
      count: number;
      probability: number;
      range_str: string;
    }> = [];
    const n = this.getIntervalsCount(data);
    const minVal = Math.min(...data);
    const step = this.getRange(data) / n;

    let iterVal = minVal;

    for (let i = 0; i < n; i++) {
      const lowerBound = iterVal;
      iterVal += step;
      const upperBound = iterVal;

      let avg = 0;
      let count = 0;

      let rangeStr = `${lowerBound.toFixed(2)}; ${upperBound.toFixed(2)}`;

      if (i === 0) {
        rangeStr = `[${rangeStr}]`;
      } else {
        rangeStr = `(${rangeStr}]`;
      }

      for (const j of data) {
        if (i === 0) {
          if (j < upperBound) {
            avg += j;
            count++;
          }
        } else if (i === n - 1) {
          if (j >= lowerBound) {
            avg += j;
            count++;
          }
        } else {
          if (lowerBound <= j && j < upperBound) {
            avg += j;
            count++;
          }
        }
      }

      avg /= count || 1;

      result.push({
        min: lowerBound,
        max: upperBound,
        avg: avg,
        count: count,
        probability: count / data.length,
        range_str: rangeStr,
      });
    }

    return result;
  }

  public getAverage(data: number[]): number {
    const series = this.getIntervalStatSeries(data);
    let avg = 0;

    for (const interval of series) {
      avg += interval.avg * interval.probability;
    }

    return avg;
  }

  public getModalIntervals(data: number[]): number[] {
    const series = this.getIntervalStatSeries(data);
    const maxCount = Math.max(...series.map((e) => e.count));
    const modeIntervals: number[] = [];

    for (let i = 0; i < series.length; i++) {
      if (series[i].count === maxCount) {
        modeIntervals.push(i);
      }
    }

    return modeIntervals.length === series.length ? [] : modeIntervals;
  }

  public getMode(data: number[]): number[] | null {
    const series = this.getIntervalStatSeries(data);
    const step = this.getRange(data) / this.getIntervalsCount(data);
    const modeIntervals = this.getModalIntervals(data);
    const modes: number[] = [];

    for (const interval of modeIntervals) {
      const mPrev = interval > 0 ? series[interval - 1].count : 0;
      const m = series[interval].count;
      const mNext =
        interval < series.length - 1 ? series[interval + 1].count : 0;

      const mode =
        series[interval].min + ((m - mPrev) / (2 * m - mPrev - mNext)) * step;
      modes.push(mode);
    }

    return modes.length === 1 ? [modes[0]] : modes.length > 0 ? modes : null;
  }

  public exactMode(data: number[]): number[] | null {
    const unique = [...new Set(data)];
    const occurrences: { [key: number]: number } = {};

    unique.forEach((element) => {
      occurrences[element] = data.filter((value) => value === element).length;
    });

    const maxCount = Math.max(...Object.values(occurrences));
    const mode: number[] = Object.keys(occurrences)
      .filter((key) => occurrences[Number(key)] === maxCount)
      .map(Number);

    if (mode.length === 1) return [mode[0]];
    if (mode.length === data.length) return null;
    return mode;
  }

  public exactMedian(data: number[]): number {
    const sortedData = [...data].sort((a, b) => a - b);
    const middle = Math.floor(sortedData.length / 2);

    return sortedData.length % 2 === 0
      ? (sortedData[middle - 1] + sortedData[middle]) / 2
      : sortedData[middle];
  }

  public distributionFunction(data: number[], x: number): number {
    const series = this.getIntervalStatSeries(data);
    if (x < series[0].min) return 0;
    if (x >= series[series.length - 1].max) return 1;

    let probability = 0;
    for (const interval of series) {
      probability += interval.probability;
      if (interval.min < x && x <= interval.max) {
        return probability;
      }
    }
    return probability;
  }

  public getMedianInterval(data: number[]): [number, number] {
    const series = this.getIntervalStatSeries(data);
    const size = data.length;
    let minVal = 0;
    let maxVal = 0;

    const mid = size % 2 === 0 ? [size / 2, size / 2 + 1] : [(size + 1) / 2];

    let total = 0;

    if (mid.length === 1) {
      for (const interval of series) {
        total += interval.count;
        if (total >= mid[0]) {
          minVal = interval.min;
          maxVal = interval.max;
          break;
        }
      }
    } else {
      for (const interval of series) {
        total += interval.count;
        if (mid[0] <= total && total < mid[1]) {
          minVal = interval.min;
          maxVal = series[series.indexOf(interval) + 1].max;
          break;
        } else if (total >= mid[1]) {
          minVal = interval.min;
          maxVal = interval.max;
          break;
        }
      }
    }

    return [minVal, maxVal];
  }

  public median(data: number[]): number {
    const [minVal, maxVal] = this.getMedianInterval(data);
    return (
      minVal +
      ((0.5 - this.distributionFunction(data, minVal)) /
        (this.distributionFunction(data, maxVal) -
          this.distributionFunction(data, minVal))) *
        (maxVal - minVal)
    );
  }

  public median2(data: number[]): number {
    const series = this.getIntervalStatSeries(data);
    const size = data.length;

    let minVal = 0;
    let maxVal = 0;
    let intervalCount = 0;
    let cumulatedTotal = 0;

    const mid = size % 2 === 0 ? [size / 2, size / 2 + 1] : [(size + 1) / 2];

    let total = 0;

    if (mid.length === 1) {
      for (const interval of series) {
        cumulatedTotal = total;
        total += interval.count;

        if (total >= mid[0]) {
          minVal = interval.min;
          maxVal = interval.max;
          intervalCount = interval.count;
          break;
        }
      }
    } else {
      for (const interval of series) {
        cumulatedTotal = total;
        total += interval.count;

        if (mid[0] <= total && total < mid[1]) {
          minVal = interval.min;
          maxVal = series[series.indexOf(interval) + 1].max;
          intervalCount = interval.count;
          break;
        } else if (total >= mid[1]) {
          minVal = interval.min;
          maxVal = interval.max;
          intervalCount = interval.count;
          break;
        }
      }
    }

    return (
      minVal + ((maxVal - minVal) / intervalCount) * (size / 2 - cumulatedTotal)
    );
  }

  public getDispersion(data: number[]): number {
    const avg = this.getAverage(data);
    const series = this.getIntervalStatSeries(data);
    let dispersion = 0;

    for (const interval of series) {
      dispersion += (interval.avg - avg) ** 2 * interval.probability;
    }

    return dispersion;
  }

  public squareDeviation(data: number[]): number {
    return Math.sqrt(this.getDispersion(data));
  }

  public adjustedDispersion(data: number[]): number {
    const series = this.getIntervalStatSeries(data);
    const n = series.length;
    return this.getDispersion(data) * (n / (n - 1));
  }

  public adjustedSquareDeviation(data: number[]): number {
    return Math.sqrt(this.adjustedDispersion(data));
  }

  public variation(data: number[]): number {
    return this.squareDeviation(data) / this.getAverage(data);
  }

  public startingMoment(data: number[], k: number): number {
    const series = this.getIntervalStatSeries(data);
    let moment = 0;

    for (const interval of series) {
      moment += interval.avg ** k * interval.probability;
    }

    return moment;
  }

  public centralMoment(data: number[], k: number): number {
    const series = this.getIntervalStatSeries(data);
    let moment = 0;
    const avg = this.getAverage(data);

    for (const interval of series) {
      moment += (interval.avg - avg) ** k * interval.probability;
    }

    return moment;
  }

  public skewness(data: number[]): number {
    return this.centralMoment(data, 3) / this.squareDeviation(data) ** 3;
  }

  public kurtosis(data: number[]): number {
    return this.centralMoment(data, 4) / this.squareDeviation(data) ** 4 - 3;
  }

  // getIntervalsCount(data: number[]): number {
  //   return Math.round(1 + 3.322 * Math.log10(data.length));
  // }

  // getRange(data: number[]): number {
  //   return Math.max(...data) - Math.min(...data);
  // }

  // getIntervalStatSeries(data: number[]) {
  //   const result = [];
  //   const n = this.getIntervalsCount(data);
  //   const minVal = Math.min(...data);
  //   const step = this.getRange(data) / n;

  //   let iterVal = minVal;

  //   for (let i = 0; i < n; i++) {
  //     const lowerBound = iterVal;
  //     iterVal += step;
  //     const upperBound = iterVal;

  //     let avg = 0;
  //     let count = 0;
  //     let rangeStr = `${lowerBound.toFixed(2)}; ${upperBound.toFixed(2)}`;

  //     if (i === 0) {
  //       rangeStr = `[${rangeStr}]`;
  //     } else {
  //       rangeStr = `(${rangeStr}]`;
  //     }

  //     for (const val of data) {
  //       if (i === 0 && val < upperBound) {
  //         avg += val;
  //         count++;
  //       } else if (i === n - 1 && val >= lowerBound) {
  //         avg += val;
  //         count++;
  //       } else if (lowerBound <= val && val < upperBound) {
  //         avg += val;
  //         count++;
  //       }
  //     }

  //     avg /= count;

  //     result.push({
  //       min: lowerBound,
  //       max: upperBound,
  //       avg: avg,
  //       count: count,
  //       probability: count / data.length,
  //       rangeStr: rangeStr,
  //     });
  //   }

  //   return result;
  // }

  // getAverage(data: number[]): number {
  //   const series = this.getIntervalStatSeries(data);
  //   let avg = 0;
  //   for (const interval of series) {
  //     avg += interval.avg * interval.probability;
  //   }
  //   return avg;
  // }

  // getModalIntervals(data: number[]): number[] {
  //   const series = this.getIntervalStatSeries(data);
  //   let maxCount = 0;
  //   const modeIntervals: number[] = [];

  //   for (const element of series) {
  //     if (element.count > maxCount) {
  //       maxCount = element.count;
  //     }
  //   }

  //   series.forEach((element, i) => {
  //     if (element.count === maxCount) {
  //       modeIntervals.push(i);
  //     }
  //   });

  //   return modeIntervals.length === series.length ? [] : modeIntervals;
  // }

  // getMode(data: number[]) {
  //   const series = this.getIntervalStatSeries(data);
  //   const step = this.getRange(data) / this.getIntervalsCount(data);
  //   const modeIntervals = this.getModalIntervals(data);
  //   const modes = [];

  //   for (const interval of modeIntervals) {
  //     let mPrev = 0,
  //       m = series[interval].count,
  //       mNext = 0;

  //     if (interval !== 0) {
  //       mPrev = series[interval - 1].count;
  //     }

  //     if (interval !== series.length - 1) {
  //       mNext = series[interval + 1].count;
  //     }

  //     const mode =
  //       series[interval].min + ((m - mPrev) / (2 * m - mPrev - mNext)) * step;
  //     modes.push(mode);
  //   }

  //   return modes.length === 1 ? modes[0] : modes.length ? modes : null;
  // }

  // exactMode(data: number[]): number | number[] | null {
  //   const occurrences: { [key: number]: number } = {};

  //   data.forEach((val) => {
  //     occurrences[val] = (occurrences[val] || 0) + 1;
  //   });

  //   const maxCount = Math.max(...Object.values(occurrences));

  //   const mode = Object.keys(occurrences)
  //     .filter((key) => occurrences[Number(key)] === maxCount)
  //     .map(Number);

  //   if (mode.length === 1) {
  //     return mode[0];
  //   } else if (mode.length === data.length) {
  //     return null;
  //   } else {
  //     return mode;
  //   }
  // }

  // exactMedian(data: number[]): number {
  //   const sortedData = [...data].sort((a, b) => a - b);
  //   const middle = Math.floor(sortedData.length / 2);

  //   if (sortedData.length % 2 === 0) {
  //     return (sortedData[middle - 1] + sortedData[middle]) / 2;
  //   } else {
  //     return sortedData[middle];
  //   }
  // }

  // getDispersion(data: number[]): number {
  //   const avg = this.getAverage(data);
  //   const series = this.getIntervalStatSeries(data);
  //   let dispersion = 0;

  //   for (const interval of series) {
  //     dispersion += Math.pow(interval.avg - avg, 2) * interval.probability;
  //   }

  //   return dispersion;
  // }

  // squareDeviation(data: number[]): number {
  //   return Math.sqrt(this.getDispersion(data));
  // }
}
