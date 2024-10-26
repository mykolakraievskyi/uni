import { Component, OnInit } from '@angular/core';
import { IntervalStatisticalSeriesService } from '../../services/interval-statistical-series.service';
import { BaseChartDirective } from 'ng2-charts';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { Chart, ChartData, ChartOptions } from 'chart.js';
import {
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  Title,
  Tooltip,
  Legend,
  BarController,
  BarElement,
  CategoryScale,
} from 'chart.js';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { IntervalStatSeries } from '../../models/interval-stat-series.model';

Chart.register(
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  Title,
  Tooltip,
  Legend,
  CategoryScale,
  BarController,
  BarElement
);

@Component({
  selector: 'smda-interval-statistical-series-tab',
  standalone: true,
  imports: [
    BaseChartDirective,
    FormsModule,
    MatSlideToggleModule,
    CommonModule,
  ],
  templateUrl: './interval-statistical-series-tab.component.html',
  styleUrl: './interval-statistical-series-tab.component.css',
})
export class IntervalStatisticalSeriesTabComponent implements OnInit {
  data: number[] = [
    0.38, 0.37, 0.38, 0.36, 0.4, 0.36, 0.48, 0.14, 0.28, 0.31, 0.57, 0.65, 0.78,
    0.7, 0.52, 0.19, 0.11, 0.0, 0.57, 0.42, 0.25,
  ];
  labels: string[] = [];

  average: number = 0;
  modalInterval: number[] = [];
  mode: number[] | null = [];
  exactMode: number[] | null = [];
  median: number = 0;
  exactMedian: number = 0;
  median2: number = 0;
  medianInterval: [number, number] = [0, 0];
  dispersion: number = 0;
  adjustedDispersion: number = 0;
  deviation: number = 0;
  adjustedSquareDeviation: number = 0;
  variation: number = 0;
  range: number = 0;
  secondOrderInitialMoment: number = 0;
  secondOrderCentralMoment: number = 0;
  kurtosis: number = 0;
  skewness: number = 0;

  series: IntervalStatSeries[] = [];
  current: number = 1;

  chartData: ChartData<'line' | 'bar'> = {
    datasets: ([] = []),
    labels: [
      {
        label: 'Frequency Histogram',
        data: [],
        fill: false,
        borderColor: 'rgba(75,192,192,1)',
        tension: 0.1,
        spanGaps: true,
        stepped: true,
      },
    ],
  };
  chartOptions: ChartOptions<'line' | 'bar'> = {
    responsive: true,
    scales: {
      x: {
        title: {
          display: true,
          text: 'Values',
        },
        ticks: {
          precision: 2,
        },
      },
      y: {
        title: {
          display: true,
          text: 'Frequency',
        },
        beginAtZero: true,
        ticks: {
          precision: 2,
        },
      },
    },
    plugins: {
      legend: {
        display: true,
      },
    },
  };
  chartType: 'bar' | 'line' = 'bar';
  public isRelative: boolean = false;

  constructor(
    private intervalStatisticalSeriesService: IntervalStatisticalSeriesService
  ) {
    this.initializeStatistics(this.data);
  }
  ngOnInit(): void {
    this.switchChart(3);
  }

  private initializeStatistics(data: number[]): void {
    this.series =
      this.intervalStatisticalSeriesService.getIntervalStatSeries(data);
    this.average = this.intervalStatisticalSeriesService.getAverage(data);
    this.modalInterval =
      this.intervalStatisticalSeriesService.getModalIntervals(data);
    this.mode = this.intervalStatisticalSeriesService.getMode(data);
    this.exactMode = this.intervalStatisticalSeriesService.exactMode(data);
    this.median = this.intervalStatisticalSeriesService.median(data);
    this.exactMedian = this.intervalStatisticalSeriesService.exactMedian(data);
    this.median2 = this.intervalStatisticalSeriesService.median2(data);
    this.medianInterval =
      this.intervalStatisticalSeriesService.getMedianInterval(data);
    this.medianInterval = [
      Number(this.medianInterval[0].toFixed(2)),
      Number(this.medianInterval[1].toFixed(2)),
    ];
    this.dispersion = this.intervalStatisticalSeriesService.getDispersion(data);
    this.adjustedDispersion =
      this.intervalStatisticalSeriesService.adjustedDispersion(data);
    this.deviation =
      this.intervalStatisticalSeriesService.squareDeviation(data);
    this.adjustedSquareDeviation =
      this.intervalStatisticalSeriesService.adjustedSquareDeviation(data);
    this.variation = this.intervalStatisticalSeriesService.variation(data);
    this.range = this.intervalStatisticalSeriesService.getRange(data);
    this.secondOrderInitialMoment =
      this.intervalStatisticalSeriesService.startingMoment(data, 2);
    this.secondOrderCentralMoment =
      this.intervalStatisticalSeriesService.centralMoment(data, 2);
    this.kurtosis = this.intervalStatisticalSeriesService.kurtosis(data);
    this.skewness = this.intervalStatisticalSeriesService.skewness(data);
  }

  switchChart(graphNumber: number) {
    this.current = graphNumber;
    switch (graphNumber) {
      case 1:
        this.frequencyHistogram();
        break;
      case 2:
        this.relativeFrequencyHistogram();
        break;
      case 3:
        this.cumulativeFrequencyCurve();
        break;
      case 4:
        this.cumulativeRelativeFrequencyCurve();
        break;
      case 5:
        this.empiricalDistributionFunction();
        break;
    }
  }

  frequencyHistogram() {
    const series = this.intervalStatisticalSeriesService.getIntervalStatSeries(
      this.data
    );
    const step =
      this.intervalStatisticalSeriesService.getRange(this.data) /
      this.intervalStatisticalSeriesService.getIntervalsCount(this.data);

    const x = series.map((interval) =>
      ((interval.max + interval.min) / 2).toPrecision(4)
    );
    const y = series.map((interval) => interval.count / step);

    this.chartData = {
      labels: x,
      datasets: [
        {
          label: 'Frequency Histogram',
          data: y,
          backgroundColor: 'rgba(75, 192, 192, 0.2)',
          borderColor: 'rgba(75, 192, 192, 1)',
          borderWidth: 1,
        },
      ],
    };

    this.chartType = 'bar';
  }

  relativeFrequencyHistogram() {
    const series = this.intervalStatisticalSeriesService.getIntervalStatSeries(
      this.data
    );
    const step =
      this.intervalStatisticalSeriesService.getRange(this.data) /
      this.intervalStatisticalSeriesService.getIntervalsCount(this.data);

    const x = series.map((interval) =>
      ((interval.max + interval.min) / 2).toPrecision(4)
    );
    const y = series.map((interval) => interval.probability / step);

    this.chartData = {
      labels: x,
      datasets: [
        {
          label: 'Relative Frequency Histogram',
          data: y,
          backgroundColor: 'rgba(153, 102, 255, 0.2)',
          borderColor: 'rgba(153, 102, 255, 1)',
          borderWidth: 1,
        },
      ],
    };

    this.chartType = 'bar';
  }

  cumulativeFrequencyCurve() {
    const series = this.intervalStatisticalSeriesService.getIntervalStatSeries(
      this.data
    );
    let sum = 0;

    const x = series.map((interval) => interval.avg.toPrecision(4));
    const y = series.map((interval) => {
      sum += interval.count;
      return sum;
    });

    this.chartData = {
      labels: x,
      datasets: [
        {
          label: 'Cumulative Frequency Curve',
          data: y,
          fill: false,
          borderColor: 'rgba(54, 162, 235, 1)',
          tension: 0.1,
        },
      ],
    };

    this.chartType = 'line';
  }

  cumulativeRelativeFrequencyCurve() {
    const series = this.intervalStatisticalSeriesService.getIntervalStatSeries(
      this.data
    );
    let sum = 0;

    const x = series.map((interval) => interval.avg.toPrecision(4));
    const y = series.map((interval) => {
      sum += interval.probability;
      return sum;
    });

    this.chartData = {
      labels: x,
      datasets: [
        {
          label: 'Cumulative Relative Frequency Curve',
          data: y,
          fill: false,
          borderColor: 'rgba(255, 159, 64, 1)',
          tension: 0.1,
        },
      ],
    };

    this.chartType = 'line';
  }

  empiricalDistributionFunction() {
    const series = this.intervalStatisticalSeriesService.getIntervalStatSeries(
      this.data
    );
    const step =
      this.intervalStatisticalSeriesService.getRange(this.data) /
      this.intervalStatisticalSeriesService.getIntervalsCount(this.data);
    let sum = 0;

    const x = [series[0].min];
    const y = [0];

    for (const interval of series) {
      sum += interval.probability;
      x.push(interval.max);
      y.push(sum);
    }

    const extendedX = [x[0] - step / 2, ...x, x[x.length - 1] + step / 2].map(
      (x) => x.toPrecision(4)
    );
    const extendedY = [0, ...y, 1];

    this.chartData = {
      labels: extendedX,
      datasets: [
        {
          label: 'Empirical Distribution Function',
          data: extendedY,
          fill: false,
          stepped: true,
          borderColor: 'rgba(75, 192, 192, 1)',
          borderWidth: 2,
        },
      ],
    };

    this.chartType = 'line';
    // this.chartOptions = {
    //   scales: {
    //     x: { beginAtZero: true },
    //     y: { beginAtZero: true },
    //   },
    // };
  }

  onValueChanged(): void {
    const xScale = this.chartOptions?.scales?.['x'] ?? {};
    xScale.type = this.isRelative ? 'linear' : 'category';
    this.switchChart(this.current);
  }
}
