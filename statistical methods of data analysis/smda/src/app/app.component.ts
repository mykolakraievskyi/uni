import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { StatisticsTabComponent } from './components/statistics-tab/statistics-tab.component';
import { MatTabsModule } from '@angular/material/tabs';
import { HeaderComponent } from './components/header/header.component';
import { IntervalStatisticalSeriesTabComponent } from './components/interval-statistical-series-tab/interval-statistical-series-tab.component';
import { IntervalEstimatesTabComponent } from './components/interval-estimates-tab/interval-estimates-tab.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    StatisticsTabComponent,
    MatTabsModule,
    HeaderComponent,
    IntervalStatisticalSeriesTabComponent,
    IntervalEstimatesTabComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'smda';
}
