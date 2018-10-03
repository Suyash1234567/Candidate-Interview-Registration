import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ServicefirstService } from '../servicefirst.service';
import { DataServiceService } from '../data-service.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-newpage',
  templateUrl: './newpage.component.html',
  styleUrls: ['./newpage.component.css']
})
export class NewpageComponent implements OnInit {
  //data: any;
  data;
  constructor(private router: Router, private ServicefirstService: ServicefirstService, private dataService:DataServiceService,private toastr: ToastrService) {
    this.data =ServicefirstService.data
    
    if(this.data == null){
      this.redirect()
    }
  }

  ngOnInit() {
    
  }

  redirect() {
    //console.log('we');
    this.router.navigate(['/']);
  }

  register(){
    
    this.dataService.registerCandidate(this.data).subscribe(res=>{ 
      
      //console.log(res);  
      
    },err=>{
      //console.log(err.error.text)
      if(err.error.text=="Register Successful")
      {
      this.toastr.success("Candidate Registered Successfully", 'Candidate Register')
      this.router.navigate(['/']);
      window.location.reload();
      }
      if(err.error.text =="Email already exist") 
      {
      this.toastr.error("Email Already Exists!! Please enter different email to register",'Candidate Register')
      this.router.navigate(['/']);
      }
    });
    
    
  }


}
