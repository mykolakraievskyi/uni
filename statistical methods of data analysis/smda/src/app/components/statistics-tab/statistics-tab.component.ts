import { Component, OnInit } from '@angular/core';
import { StatisticsService } from '../../services/statistics.service';
import { Chart, ChartData, ChartOptions } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  Title,
  Tooltip,
  Legend,
  CategoryScale,
} from 'chart.js';
import { FormsModule } from '@angular/forms';

Chart.register(
  LineController,
  LineElement,
  PointElement,
  LinearScale,
  Title,
  Tooltip,
  Legend,
  CategoryScale
);

@Component({
  selector: 'app-statistics-tab',
  templateUrl: './statistics-tab.component.html',
  styleUrls: ['./statistics-tab.component.css'],
  standalone: true,
  imports: [BaseChartDirective, FormsModule, MatSlideToggleModule],
})
export class StatisticsTabComponent implements OnInit {
  data: number[] = [0.3, 0.29, 0.32, 0.36, 0.37, 0.37, 0.42, 0.4];
  public average: number = 0;
  public median: number = 0;
  public mode: number[] | null = null;
  public std: number = 0;
  public range: number = 0;
  public dispersion: number = 0;
  public correctedDispersion: number = 0;
  public correctedSTD: number = 0;
  public variation: number = 0;
  public asymmetry: number = 0;
  public excess: number = 0;
  public cm: number = 0;
  public pm: number = 0;

  public isRelative: boolean = false;
  private functionArray: Function[] = [
    this.showPolygon,
    this.showKumulCurve,
    this.showPoligonRel,
    this.showKumulCurveRel,
    this.showEmpiricalFunc,
  ];
  private current: Function = this.functionArray[0];

  public get inputDataString() {
    return this.data.join(', ');
  }

  public set inputDataString(value: string) {
    this.data = value.split(',').map((str) => parseFloat(str));
    this.current();
  }

  chartData: ChartData<'line'> = {
    labels: [],
    datasets: [
      {
        label: 'Frequency Polygon',
        data: [],
        fill: false,
        borderColor: 'rgba(75,192,192,1)',
        tension: 0.1,
        spanGaps: true,
        stepped: true,
      },
    ],
  };

  chartOptions: ChartOptions<'line'> = {
    responsive: true,
    scales: {
      x: {
        // type: 'linear',
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

  constructor(private statisticsService: StatisticsService) {}

  ngOnInit(): void {
    this.calculateStatistics();
    this.showPolygon();
  }

  private calculateStatistics(): void {
    this.average = this.statisticsService.getAverage(this.data);
    this.median = this.statisticsService.getMedian(this.data);
    this.mode = this.statisticsService.getMode(this.data);
    this.std = this.statisticsService.getSTD(this.data);
    this.range = this.statisticsService.getRange(this.data);
    this.dispersion = this.statisticsService.getDispersion(this.data);
    this.correctedDispersion = this.statisticsService.getCorrectedDispersion(
      this.data
    );
    this.correctedSTD = this.statisticsService.getCorrectedSTD(this.data);
    this.variation = this.statisticsService.getVariation(this.data);
    this.asymmetry = this.statisticsService.getAsymmetry(this.data);
    this.excess = this.statisticsService.getExcess(this.data);
    this.pm = this.statisticsService.getPM(this.data, 2);
    this.cm = this.statisticsService.getCM(this.data, 2);
  }

  private updateChartData(
    xdata: number[],
    ydata: number[],
    label: string,
    stepped: boolean = false
  ): void {
    this.chartData.labels = xdata.map((val) => val.toFixed(2));
    this.chartData.datasets[0].data = ydata;
    this.chartData.datasets[0].label = label;
    this.chartData.datasets[0].stepped = stepped;
  }

  showPolygon() {
    this.current = this.showPolygon;

    const sortedData = [...new Set(this.data)].sort();
    const frequencies = sortedData.map(
      (value) => this.data.filter((d) => d === value).length
    );

    this.updateChartData(sortedData, frequencies, 'Frequency Polygon');
  }

  showKumulCurve() {
    this.current = this.showPoligonRel;
    const sortedData = [...new Set(this.data)].sort();
    const cumulativeFrequencies = sortedData.reduce((acc: number[], value) => {
      const frequency = this.data.filter((d) => d === value).length;
      const sum = acc.length > 0 ? acc[acc.length - 1] + frequency : frequency;
      acc.push(sum);
      return acc;
    }, []);

    this.updateChartData(
      sortedData,
      cumulativeFrequencies,
      'Cumulative Frequency Curve'
    );
  }

  showPoligonRel() {
    this.current = this.showPoligonRel;
    const sortedData = [...new Set(this.data)].sort();
    const total = this.data.length;
    const relativeFrequencies = sortedData.map(
      (value) => this.data.filter((d) => d === value).length / total
    );

    this.updateChartData(
      sortedData,
      relativeFrequencies,
      'Relative Frequency Polygon'
    );
  }

  showKumulCurveRel() {
    this.current = this.showKumulCurveRel;
    const sortedData = [...new Set(this.data)].sort();
    const total = this.data.length;
    const relativeFrequencies = sortedData.map(
      (value) => this.data.filter((d) => d === value).length / total
    );
    const cumulativeRelativeFrequencies = relativeFrequencies.reduce(
      (acc: number[], value) => {
        const sum = acc.length > 0 ? acc[acc.length - 1] + value : value;
        acc.push(sum);
        return acc;
      },
      []
    );

    this.updateChartData(
      sortedData,
      cumulativeRelativeFrequencies,
      'Cumulative Relative Frequency Curve'
    );
  }

  showEmpiricalFunc() {
    this.current = this.showEmpiricalFunc;
    const sortedData = this.data.sort((a, b) => a - b);

    const n = sortedData.length;
    const xValues = sortedData;
    const yValues = sortedData.map((_, index) => (index + 1) / n);

    this.updateChartData(
      xValues,
      yValues,
      'Empirical Cumulative Distribution Function',
      true
    );
  }

  onValueChanged(): void {
    const xScale = this.chartOptions?.scales?.['x'] ?? {};
    xScale.type = this.isRelative ? 'linear' : 'category';
    console.log(this.chartOptions?.scales?.['x']);
    this.current();
  }
}
