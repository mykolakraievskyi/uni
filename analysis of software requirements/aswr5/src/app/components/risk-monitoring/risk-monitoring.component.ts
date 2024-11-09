import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DataService } from '../../services/data.service';
import { AnalysisData, MonitoringData } from '../../models/models';
import { MatTableModule } from '@angular/material/table';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { CapitalizePipe } from '../../pipes/capitalize.pipe';
import { Type, Status } from '../../models/enums';
import { UkrainianNumberDirective } from '../../directives/ukrainian-number.directive';

@Component({
  selector: 'app-risk-monitoring',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    CapitalizePipe,
    MatButtonToggleModule,
    MatTableModule,
    UkrainianNumberDirective,
  ],
  templateUrl: './risk-monitoring.component.html',
  styleUrl: './risk-monitoring.component.scss',
})
export class RiskMonitoringComponent {
  perName: string[] = [
    'erper0',
    'erper1',
    'erper2',
    'erper3',
    'erper4',
    'erper5',
    'erper6',
    'erper7',
    'erper8',
    'erper9',
  ];
  columnsToDisplay: string[] = ['id', 'name', ...this.perName, 'erer'];
  columnsToDisplayEvaluation: string[] = [
    'id',
    'name',
    'erer',
    'elrer',
    'evrer',
    'priority',
  ];
  statuses = Status;
  type = Type;
  chosenType: Type = Type.Probability;
  data: MonitoringData[];

  constructor(private dataService: DataService) {
    this.data = dataService.monitoringData;
    this.recalculate();
  }

  recalculate() {
    this.dataService.recalculate();
  }

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
}
