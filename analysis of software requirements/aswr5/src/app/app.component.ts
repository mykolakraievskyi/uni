import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { RiskAnalysisComponent } from './components/risk-analysis/risk-analysis.component';
import { RiskIdentificationComponent } from './components/risk-identification/risk-identification.component';
import { RiskMonitoringComponent } from './components/risk-monitoring/risk-monitoring.component';
import { RiskPlanningComponent } from './components/risk-planning/risk-planning.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    CommonModule,
    MatTabsModule,
    RiskIdentificationComponent,
    RiskAnalysisComponent,
    RiskMonitoringComponent,
    RiskPlanningComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'aswr5';
}
