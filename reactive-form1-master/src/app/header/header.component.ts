import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataServiceService } from '../data-service.service';
import { ServicefirstService } from '../servicefirst.service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isValid:boolean
  userData

  constructor(private route: Router, private myservice: ServicefirstService) {
       
  }

  ngOnInit() {
    this.myservice.mydata.subscribe(res=>{
      this.isValid =res
    })

    this.myservice.user.subscribe(res=>{   
      this.userData =res
     this.userData.employeeName= this.userData.employeeName.toUpperCase();
    })
  }
  Redirect() {
    this.route.navigate(['/loginPage']);
  }

  router()
  {
  if(localStorage.length==0)
  {
    this.route.navigate(['/']);
    window.location.reload(); 
  }
  else if(localStorage!=null)
  {
    this.route.navigate(['/details']);
  }
  else
  {
    this.route.navigate(['/']);
    window.location.reload();
  }
  }
  logout(){
    localStorage.clear();
    this.isValid =false
    this.userData =null
    this.route.navigate(['/loginPage']);
  }
}
