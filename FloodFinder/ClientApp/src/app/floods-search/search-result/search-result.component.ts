import { Component, Input, OnInit } from '@angular/core';
import { FloodWarningSearchResult } from '../flood-search.service';

@Component({
  selector: 'app-search-result',
  templateUrl: './search-result.component.html',
  styleUrls: ['./search-result.component.scss']
})
export class SearchResultComponent implements OnInit {

  @Input() result: FloodWarningSearchResult;

  constructor() { }

  ngOnInit() {
  }

}
