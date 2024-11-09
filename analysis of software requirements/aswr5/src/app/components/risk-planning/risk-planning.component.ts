import { Component } from '@angular/core';
import { IdentificationData } from '../../models/models';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { CapitalizePipe } from '../../pipes/capitalize.pipe';
import { PlanningData } from '../../models/models';
import { Type } from '../../models/enums';
import { DataService } from '../../services/data.service';
@Component({
  selector: 'app-risk-planning',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    MatButtonToggleModule,
    CapitalizePipe,
  ],
  templateUrl: './risk-planning.component.html',
  styleUrl: './risk-planning.component.scss',
})
export class RiskPlanningComponent {
  columnsToDisplay: string[] = ['id', 'name', 'value'];
  projectEvents: string[];
  risks: PlanningData[];

  constructor(private dataService: DataService) {
    this.projectEvents = dataService.planningDataEvents;
    this.risks = dataService.planningData;
  }
}
