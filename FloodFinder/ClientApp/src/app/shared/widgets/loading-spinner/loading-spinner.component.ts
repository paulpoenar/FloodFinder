import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { delay } from 'rxjs/operators';
import { ApplicationMessagesService } from '../../services/application-messages.service';

@Component({
  selector: 'app-loading-spinner',
  templateUrl: './loading-spinner.component.html',
  styleUrls: ['./loading-spinner.component.scss']
})
export class LoadingSpinnerComponent implements OnInit {

  public showSpinner$: Observable<boolean>;

  constructor(
    private appMessagesService: ApplicationMessagesService) {}

  ngOnInit() {
    this.showSpinner$ = this.appMessagesService.loadingSpinnerToggle$
    .pipe(delay(0));
  }

}
