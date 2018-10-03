import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataServiceService } from '../data-service.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isValid:boolean;

  constructor(private route: Router, private dataService: DataServiceService) {
   
    
  }



  ngOnInit() {
    this.isValid = this.dataService.getButtonStatus();
    console.log(this.isValid);
  }
  Redirect() {
    this.route.navigate(['/loginPage']);
  }
}
