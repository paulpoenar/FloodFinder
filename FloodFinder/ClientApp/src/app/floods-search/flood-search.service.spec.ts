import { TestBed } from '@angular/core/testing';

import { FloodSearchService } from './flood-search.service';

describe('FloodSearchService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FloodSearchService = TestBed.get(FloodSearchService);
    expect(service).toBeTruthy();
  });
});
