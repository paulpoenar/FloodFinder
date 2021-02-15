import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateFormat'
})
export class DateFormatPipe extends DatePipe implements PipeTransform {

  transform(value: any, args?: string): any {
    if (value) {
      if (args && args === 'showTime') {
        const date = super.transform(new Date(value), 'dd/MM/yyyy');
        const time = super.transform(new Date(value), 'HH:mm');
        return date + ' - ' + time;
      }
      return super.transform(new Date(value), 'dd/MM/yyyy');
    }
    return '';
  }

}
