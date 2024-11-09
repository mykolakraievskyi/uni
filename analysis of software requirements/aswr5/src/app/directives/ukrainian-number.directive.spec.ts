import { UkrainianNumberDirective } from './ukrainian-number.directive';
import { ElementRef } from '@angular/core';

describe('UkrainianNumberDirective', () => {
  it('should create an instance', () => {
    const elementRefMock = new ElementRef(document.createElement('input'));
    const directive = new UkrainianNumberDirective(elementRefMock);
    expect(directive).toBeTruthy();
  });
});
