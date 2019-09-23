import { TestBed } from '@angular/core/testing';

import { FcnService } from './fcn.service';

describe('FcnService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FcnService = TestBed.get(FcnService);
    expect(service).toBeTruthy();
  });
});
