import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CandidatedetailsHRComponent } from './candidatedetails-hr.component';

describe('CandidatedetailsHRComponent', () => {
  let component: CandidatedetailsHRComponent;
  let fixture: ComponentFixture<CandidatedetailsHRComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CandidatedetailsHRComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CandidatedetailsHRComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
