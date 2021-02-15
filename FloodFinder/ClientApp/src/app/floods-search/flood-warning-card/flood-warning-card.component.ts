import { Component, Input, OnInit } from '@angular/core';
import { FloodWarningSearchResultItem } from '../flood-search.service';

@Component({
  selector: 'app-flood-warning-card',
  templateUrl: './flood-warning-card.component.html',
  styleUrls: ['./flood-warning-card.component.scss']
})
export class FloodWarningCardComponent implements OnInit {

  @Input() item: FloodWarningSearchResultItem;

  constructor() { }

  ngOnInit() {
  }

}
