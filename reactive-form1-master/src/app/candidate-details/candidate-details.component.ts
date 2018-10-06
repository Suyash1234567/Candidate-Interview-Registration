import { Component, OnInit } from '@angular/core';
import { DataServiceService } from '../data-service.service';
import { Router } from '@angular/router';
import { empty } from 'rxjs';

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
  dotNetMarks:number;
  angularMarks:number;
  javaMarks:number;
  reactMarks:number;
  blockChainMarks:number;
  Comments:string;
  marks=[];
  //testFilePath: string;
  
  constructor(private dataService:DataServiceService,private route : Router) { 
    this.dataService.setButtonStatus(this.isValid);
  }

  ngOnInit() {
    this.candidateList=this.dataService.getAllCandidateList();
    this.candidateName=this.dataService.getCandidateName();
    this.employeeRole= this.dataService.getEmployeeRole();
    //this.testFilePath="https://images.pexels.com/photos/67636/rose-blue-flower-rose-blooms-67636.jpeg";
    if(this.employeeRole=='HR')
    {
      this.isIT=false;
    }
    
    //console.log(this.employeeRole);
    this.employee=this.dataService.getEmployee();
    this.candidateList.forEach(candidate => {
      if(candidate.candidateName==this.candidateName){
        this.candidate=candidate;
             this.dataService.getMarks(this.candidate.candidateId).subscribe(res=>{             
              if(res.length>0){
              this.dotNetMarks=res[0].marks
              this.angularMarks=res[1].marks
              this.javaMarks =res[2].marks
              this.reactMarks =res[3].marks
              this.blockChainMarks=res[4].marks
              }
             });
      }
    });
  }

  Submit()
  {
    this.marks=[{courseID:1,marks:this.dotNetMarks},{courseID:2,marks:this.angularMarks},{courseID:3,marks:this.javaMarks},{courseID:4,marks:this.reactMarks},{courseID:5,marks:this.blockChainMarks}]
    
    this.dataService.submitCandidate(this.employeeRole,this.employee.employeeId,this.candidate.candidateId,this.marks,this.Comments).subscribe(res=>{
      this.route.navigate(['/details']);  
    });
  }
}
