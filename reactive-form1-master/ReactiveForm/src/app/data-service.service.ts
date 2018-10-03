import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataServiceService {
  editForm:boolean;
  isValid:boolean;
  userName:string;
  password:string;
  employee: any;
  candidateList=[];
  candidateName:string;
  employeeRole:any;
  httpOptions={
    headers : new HttpHeaders({'content-type':'application/json'})
  }

  constructor(private http: HttpClient) { 
  }

  registerCandidate(registrationDetails):Observable<any>{
    var data={  
    "candidateName": registrationDetails.candidateName,
    "candidateEmail": registrationDetails.candidateEmail,
    "candidateAddress": registrationDetails.candidateAddress,
    "candidateHighestQualification": registrationDetails.candidateHighestQualification,
    "candidateContactNo": registrationDetails.candidateContactNo.toString(),
    "candidateResume": registrationDetails.candidateResume,
    "candidateDateOfBirth": registrationDetails.candidateDateOfBirth
  } 
    return this.http.post('http://localhost:54770/api/Candidates',data,this.httpOptions);
  }

  getDetails():Observable<any>{
    return this.http.get('http://localhost:54770/api/candidates');
  }

  getCandidateList(userName,userPassword):Observable<any>{
    var data={  
      "userName": userName,
      "userPassword": userPassword,
    }
    return this.http.post('http://localhost:54770/api/auth/AllCandidate',data);
  }

  authenticateUser(userName,userPassword):Observable<any>{
    var data={  
      "userName": userName,
      "userPassword": userPassword,
    }
    return this.http.post('http://localhost:54770/api/auth/Employee',data);
  }

  submitCandidate(EmployeeRole,EmployeeId,CandidateId):Observable<any>{
    var data={  
      "EmployeeRole": EmployeeRole,
      "EmployeeId": EmployeeId,
      "CandidateId": CandidateId
    }
    return this.http.put('http://localhost:54770/api/InterviewDetails/'+CandidateId,data);
  }

  setUserName(userName){
    this.userName=userName;
  }

  getUserName(){
    return this.userName;
  }

  setPassword(password){
    this.password=password
  }

  getPassword(){
    return this.password;
  }

  setEmployee(employee:any){
    this.employee=employee;
  }

   getEmployee(){
    return this.employee;
   }

   setAllCandidateList(candidateList){
    this.candidateList=candidateList;
   }

   getAllCandidateList(){
    return this.candidateList;
  }

  setCandidateName(candidateName){
    this.candidateName=candidateName;
  }

  getCandidateName(){
    return this.candidateName;
  }

  getEmployeeRole(){
    return this.employeeRole;
  }

  setEmployeeRole(employeeRole){
   this.employeeRole=employeeRole;
  }

  setButtonStatus(isValid){
   this.isValid=isValid;
   console.log(isValid,"set");
  }
  getButtonStatus(){
    return this.isValid;
  }
}
