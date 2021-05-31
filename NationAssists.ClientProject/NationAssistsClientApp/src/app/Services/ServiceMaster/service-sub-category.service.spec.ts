import { TestBed } from '@angular/core/testing';

import { ServiceSubCategoryService } from './service-sub-category.service';

describe('ServiceSubCategoryService', () => {
  let service: ServiceSubCategoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ServiceSubCategoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
