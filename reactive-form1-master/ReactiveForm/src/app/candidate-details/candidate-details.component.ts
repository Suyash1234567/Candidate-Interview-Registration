import { Component, OnInit } from '@angular/core';
import { DataServiceService } from '../data-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-candidate-details',
  templateUrl: './candidate-details.component.html',
  styleUrls: ['./candidate-details.component.css']
})
export class CandidateDetailsComponent implements OnInit {
  candidate:any;
  candidateList=[];
  candidateName:string;
  employee:any;
  employeeRole:string;
  isIT:boolean=true;
  isValid:boolean=true;
  constructor(private dataService:DataServiceService,private route : Router) { 
    this.dataService.setButtonStatus(this.isValid);
  }

  ngOnInit() {
    this.candidateList=this.dataService.getAllCandidateList();
    this.candidateName=this.dataService.getCandidateName();
    this.employeeRole= this.dataService.getEmployeeRole();
    if(this.employeeRole=='HR')
    {
      this.isIT=false;
      // document.getElementById("marks").disabled = true;
    }
    
    console.log(this.employeeRole);
    this.employee=this.dataService.getEmployee();
    this.candidateList.forEach(candidate => {
      if(candidate.candidateName==this.candidateName){
        this.candidate=candidate;
      }
    });
    console.log(this.candidate);
  }

  Submit()
  {
    this.dataService.submitCandidate(this.employeeRole,this.employee.employeeId,this.candidate.candidateId).subscribe(res=>{
      console.log(res);
      this.route.navigate(['/']);
    });
  }
}
