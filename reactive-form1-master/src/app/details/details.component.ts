import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataServiceService } from '../data-service.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  user:any;
  userName: string;
  password: string;
  candidateName: string;
  candidateList = [];
  employee:any;
  isValid:boolean=true;
  constructor(private router: Router, private dataService: DataServiceService) 
  {
    this.dataService.setButtonStatus(this.isValid);
   }

  ngOnInit() {
    this.userName = this.dataService.getUserName();
    this.password = this.dataService.getPassword();
    this.employee=this.dataService.getEmployee();
    this.dataService.getCandidateList(this.userName, this.password).subscribe(res => {
      this.candidateList = res;
      if(res==null)
      {
        this.router.navigate(['loginPage']);
      }

      //console.log(res);
      this.dataService.setAllCandidateList(this.candidateList);
    });
  }

  displayData(candidateName) {
    this.candidateName=candidateName;
    //console.log(candidateName);
    this.dataService.setCandidateName(this.candidateName);
    this.dataService.setEmployeeRole(this.employee.employeeRole);
    this.router.navigate(['/candidateDetails']);
  }
}
