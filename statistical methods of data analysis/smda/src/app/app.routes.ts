import { Routes } from '@angular/router';
import { StatisticsTabComponent } from './components/statistics-tab/statistics-tab.component';

export const routes: Routes = [
  { path: 'task1', component: StatisticsTabComponent },
  //{ path: 'task2', component: PlaceholderComponent }, // Placeholder for other tasks
  { path: '', redirectTo: '/task1', pathMatch: 'full' },
];
