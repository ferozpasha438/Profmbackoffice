import { Component, OnInit, Inject, LOCALE_ID } from '@angular/core';
import { formatNumber } from '@angular/common';
import { Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'decimalpipe'
})
export class DecimalPipe implements PipeTransform  {
  
  transform(value: any, args: any[]) {
    //constructor(@Inject(LOCALE_ID) private locale: string) {
    return formatNumber(value, "en", '1.2-2');
    
  }
}
