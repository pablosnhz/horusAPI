import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filter'
})
export class FilterPipe implements PipeTransform {

  transform(value: any, args?: any): any {
    if (!value) return null;
    if (!args) return value;

    return value.filter((item: any) => item[args.property].toLowerCase().includes(args.value.toLowerCase()));

}
}
