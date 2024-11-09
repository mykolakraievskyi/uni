import { Component } from '@angular/core';
import { IdentificationData } from '../../models/models';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { RiskType } from '../../models/enums';
import { CapitalizePipe } from '../../pipes/capitalize.pipe';
import { UkrainianNumberDirective } from '../../directives/ukrainian-number.directive';
import { AnalysisData } from '../../models/models';
import { Status, Type } from '../../models/enums';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-risk-analysis',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    MatButtonToggleModule,
    CapitalizePipe,
    UkrainianNumberDirective,
  ],
  templateUrl: './risk-analysis.component.html',
  styleUrl: './risk-analysis.component.scss',
})
export class RiskAnalysisComponent {
  minVrer: number = 0;
  maxVrer: number = 0;
  mpr: number = 0;
  intervals: string[] = [];
  perName: string[] = [
    'per0',
    'per1',
    'per2',
    'per3',
    'per4',
    'per5',
    'per6',
    'per7',
    'per8',
    'per9',
  ];
  columnsToDisplay: string[] = ['id', 'name', ...this.perName, 'er'];
  columnsToDisplayEvaluation: string[] = [
    'id',
    'name',
    'er',
    'lrer',
    'vrer',
    'priority',
  ];
  statuses = Status;
  type = Type;
  chosenType: Type = Type.Probability;
  data: AnalysisData[];

  getPriority(propability: number): Status {
    if (propability >= 0.75) {
      return Status.VeryHigh;
    } else if (propability >= 0.5) {
      return Status.High;
    } else if (propability >= 0.25) {
      return Status.Medium;
    } else if (propability >= 0.1) {
      return Status.Low;
    } else {
      return Status.VeryLow;
    }
  }

  constructor(private dataService: DataService) {
    this.data = dataService.analysisData;

    this.recalculate();
  }

  recalculate() {
    this.dataService.recalculate();
    this.minVrer = Math.min(...this.data.map((x) => x.vrer));
    this.maxVrer = Math.max(...this.data.map((x) => x.vrer));
    this.mpr = (this.maxVrer - this.minVrer) / 3;

    this.intervals = [
      `[${this.minVrer.toFixed(2)}; ${(this.minVrer + this.mpr).toFixed(2)})`,
      `[${(this.minVrer + this.mpr).toFixed(2)}; ${(
        this.minVrer +
        this.mpr * 2
      ).toFixed(2)})`,
      `[${(this.minVrer + this.mpr * 2).toFixed(2)}; ${this.maxVrer.toFixed(
        2
      )})`,
    ];
  }
}
