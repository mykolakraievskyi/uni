import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'comma',
  standalone: true,
})
export class CommaPipe implements PipeTransform {
  transform(value: number): string {
    return value.toString().replace('.', ',');
  }
}
