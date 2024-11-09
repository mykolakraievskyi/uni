import { Component } from '@angular/core';
import { IdentificationData } from '../../models/models';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { RiskType } from '../../models/enums';
import { CapitalizePipe } from '../../pipes/capitalize.pipe';
import { DataType } from '../../models/enums';
import { DataService } from '../../services/data.service';

@Component({
  selector: 'app-risk-identification',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    FormsModule,
    MatButtonToggleModule,
    CapitalizePipe,
  ],
  templateUrl: './risk-identification.component.html',
  styleUrl: './risk-identification.component.scss',
})
export class RiskIdentificationComponent {
  isRisk: boolean = true;
  columnsToDisplay = ['id', 'name', 'value'];
  riskTypeDict = RiskType;
  chosenType: DataType = DataType.Risk;
  risks: Record<DataType, Record<string, IdentificationData[]>>;

  headers: Record<string, string> = {
    [RiskType.tech]: 'Технічні ризики',
    [RiskType.cost]: 'Вартісні ризики',
    [RiskType.plan]: 'Планові ризики',
    [RiskType.realisation]: 'Реалізаційні ризики',
  };

  menuHeaders: Record<DataType, Record<string, string>> = {
    [DataType.Event]: {
      [RiskType.tech]: 'Технічні ризикові події',
      [RiskType.cost]: 'Вартісні ризикові події',
      [RiskType.plan]: 'Планові ризикові події',
      [RiskType.realisation]: 'Ризикові події управління',
    },
    [DataType.Risk]: {
      [RiskType.tech]: 'Технічні ризики',
      [RiskType.cost]: 'Вартісні ризики',
      [RiskType.plan]: 'Планові ризики',
      [RiskType.realisation]: 'Ризики управління',
    },
  };

  constructor(private dataService: DataService) {
    this.risks = dataService.identificationData;
  }

  getTotal(type: string): number {
    if (type != RiskType.total) {
      const count = this.risks[this.chosenType][type].length;
      const trueCount = this.risks[this.chosenType][type].filter(
        (x) => x.value
      ).length;
      return (trueCount / count) * 100;
    }

    let totalItems = 0;
    let totalTrue = 0;

    for (const riskArray of Object.values(this.risks[this.chosenType])) {
      totalItems += riskArray.length;
      totalTrue += riskArray.filter((item) => item.value).length;
    }

    return (totalTrue / totalItems) * 100;
  }
}
