import { TestBed, inject } from '@angular/core/testing';

import { ServicefirstService } from './servicefirst.service';

describe('ServicefirstService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ServicefirstService]
    });
  });

  it('should be created', inject([ServicefirstService], (service: ServicefirstService) => {
    expect(service).toBeTruthy();
  }));
});
