import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FloodsService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  public searchForWarnings(county: string){
    const url = "https://environment.data.gov.uk/flood-monitoring/id/floods?county=" + county;
    return this.http.get<FloodWarningSearchResult>(url);
  }

  public getCounties(){
    const url = this.baseUrl + 'counties';
    return this.http.get<CountyViewModel[]>(url).pipe(map((x:any)=>x.data));
  }

  public logEnquiry(countyId: number, items: FloodWarningSearchResultItem[]){
    const url = this.baseUrl + 'enquiry';
    return this.http.post(url, {countyId, items});
  }
}

export interface FloodWarningSearchResult {
  "@context": string;
  meta:       FloodWarningSearchResultMeta;
  items:      FloodWarningSearchResultItem[];
}

export interface FloodWarningSearchResultItem {
  "@id":               string;
  description:         string;
  eaAreaName:          string;
  eaRegionName:        string;
  floodArea:           FloodArea;
  floodAreaID:         string;
  isTidal:             boolean;
  message:             string;
  severity:            string;
  severityLevel:       number;
  timeMessageChanged:  Date;
  timeRaised:          Date;
  timeSeverityChanged: Date;
}

export interface FloodArea {
  "@id":      string;
  county:     string;
  notation:   string;
  polygon:    string;
  riverOrSea: string;
}

export interface FloodWarningSearchResultMeta {
  publisher:     string;
  licence:       string;
  documentation: string;
  version:       string;
  comment:       string;
  hasFormat:     string[];
}

export interface CountyViewModel{
  id: number;
  name: string;
}

