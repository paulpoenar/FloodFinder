import { OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

export class SubscriptionBase implements OnDestroy {
  protected subscription = new Subscription();

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
