import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { DataServiceService } from '../data-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  isValid : boolean=true;
  constructor(private router: Router, private dataService: DataServiceService) 
  {
    this.dataService.setButtonStatus(this.isValid);
  }
  title = "LoginPage";
  user: string;
  password: string;
  message: string;
  isValidUser: boolean=true;
  employee:any;
  ngOnInit() {

  }

  submit() {
    this.dataService.authenticateUser(this.user, this.password).subscribe(res => {
      localStorage.setItem("user","valid");
      this.dataService.setUserName(this.user);
      this.dataService.setPassword(this.password);
      this.employee=res;
      this.dataService.setEmployee(this.employee);
      this.router.navigate(['/details']);
    },
      error => {
        this.message = "Incorrect Username or Password";
        this.isValidUser=false;
      }
    );
  }
}
