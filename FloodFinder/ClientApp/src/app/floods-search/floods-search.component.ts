import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material';
import { Observable } from 'rxjs';
import { map, startWith, switchMap, tap } from 'rxjs/operators';
import { SubscriptionBase } from '../shared/base/subscription.base';
import { CountyViewModel, FloodsService, FloodWarningSearchResult } from './flood-search.service';

@Component({
  selector: 'app-floods-search',
  templateUrl: './floods-search.component.html',
  styleUrls: ['./floods-search.component.scss']
})
export class FloodsSearchComponent extends SubscriptionBase implements OnInit {

  searchResult: FloodWarningSearchResult;
  myControl = new FormControl();
  counties: CountyViewModel[] = [];
  filteredOptions: Observable<CountyViewModel[]>;

  private selectedCounty: CountyViewModel;


  constructor(private floodsService: FloodsService) { super()}

  ngOnInit() {
    this.subscription.add(
      this.floodsService.getCounties().subscribe(
        x=> this.counties = x
      )
    )

    this.filteredOptions = this.myControl.valueChanges
    .pipe(
      startWith(''),
      map(value => typeof value === 'string' ? value : value.name),
      map(name => name ? this._filter(name) : this.counties.slice())
    );
  }

  optionSelectedHandler(event: MatAutocompleteSelectedEvent){
    this.selectedCounty = event.option.value;

    this.subscription.add(
      this.floodsService.searchForWarnings(this.selectedCounty.name).pipe(
        tap(x=> {this.searchResult = x;}),
        switchMap(x=> this.floodsService.logEnquiry(this.selectedCounty.id, this.searchResult.items))
      ).subscribe()
    )
  }

  displayFn(county: CountyViewModel): string {
    return county && county.name ? county.name : '';
  }

  private _filter(name: string): CountyViewModel[] {
    const filterValue = name.toLowerCase();

    return this.counties.filter(option => option.name.toLowerCase().indexOf(filterValue) === 0);
  }

}
