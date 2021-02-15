import { TestBed } from '@angular/core/testing';

import { ApplicationMessagesService } from './application-messages.service';

describe('ApplicationMessagesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApplicationMessagesService = TestBed.get(ApplicationMessagesService);
    expect(service).toBeTruthy();
  });
});
