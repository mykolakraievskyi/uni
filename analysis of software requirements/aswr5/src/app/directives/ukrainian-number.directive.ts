import { Directive } from '@angular/core';
import { HostListener } from '@angular/core';
import { ElementRef } from '@angular/core';

@Directive({
  selector: '[appUkrainianNumber]',
  standalone: true,
})
export class UkrainianNumberDirective {
  constructor(private el: ElementRef) {}

  @HostListener('input', ['$event'])
  onInputChange(event: Event): void {
    const input = event.target as HTMLInputElement;

    // const partialPattern = /^\d+[,\.]/;
    // const ukrainianNumberPattern = /^\d+[,\.]?\d{0,2}$/;

    // if (!ukrainianNumberPattern.test(input.value)) {
    //   this.el.nativeElement.value = '0';
    //   return;
    // }

    // if (partialPattern.test(input.value)) {
    //   return;
    // }

    let num = Number(input.value);
    if (isNaN(num) || num > 1 || num < 0) {
      this.el.nativeElement.value = '0';
      return;
    }
    num = Number(num.toFixed(2));
    this.el.nativeElement.value = input.value;
  }
}
