import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import * as swal from 'sweetalert2';
import { parseErrorMessage } from '../helpers/api-response.helper';

@Injectable({
  providedIn: 'root'
})
export class ApplicationMessagesService {

  private loadingSpinnerToggle = new BehaviorSubject<boolean>(undefined);
  public loadingSpinnerToggle$ = this.loadingSpinnerToggle.asObservable();

  constructor() { }

  public showApiErrorNotification(error: any) {
    const content= parseErrorMessage(error);
    this.showSwallError(content);
  }

  public showSuccessInformationModal(text: string) {
    swal.default.fire(
      'Success!',
       text,
      'success'
    );
  }

  public showAskForConfirmationModal(title: string, text: string, callback: () => void, cancelCalback?: () => void) {
    swal.default.fire({
      title,
      text,
      confirmButtonText: 'Yes',
      cancelButtonText: 'No',
      icon: 'question',
      showCancelButton: true
    }).then((result) => {
      if (result.value) {
        callback();
      } else {
        if (cancelCalback){
          cancelCalback();
        }

      }
    });
  }

  public showDeleteConfirmation(callback: () => void, entity: string) {
    swal.default.fire({
      title: 'Delete',
      text: `Are you sure you want to delete this ${entity}?`,
      icon: 'warning',
      confirmButtonText: 'Yes',
      cancelButtonText: 'No',
      showCancelButton: true
    }).then((result) => {
      if (result.value) {
        callback();
      }
    });
  }

  public showSwallError(message: string) {
    swal.default.fire({
      title: 'Error',
      text: `${message}`,
      icon: 'error'
    });
  }

  public showProgressIndicator() {
    this.loadingSpinnerToggle.next(true);
  }

  public hideProgressIndicator() {
    this.loadingSpinnerToggle.next(false);
  }
}
