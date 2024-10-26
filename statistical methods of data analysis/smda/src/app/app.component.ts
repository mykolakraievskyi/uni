import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { StatisticsTabComponent } from './components/statistics-tab/statistics-tab.component';
import { MatTabsModule } from '@angular/material/tabs';
import { HeaderComponent } from './components/header/header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    StatisticsTabComponent,
    MatTabsModule,
    HeaderComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'smda';
}
