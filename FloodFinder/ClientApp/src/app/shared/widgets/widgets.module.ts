import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingSpinnerComponent } from './loading-spinner/loading-spinner.component';
import { NoDataComponent } from './no-data/no-data.component';



@NgModule({
  declarations: [LoadingSpinnerComponent, NoDataComponent],
  imports: [
    CommonModule
  ],
  exports: [LoadingSpinnerComponent, NoDataComponent]
})
export class WidgetsModule { }
