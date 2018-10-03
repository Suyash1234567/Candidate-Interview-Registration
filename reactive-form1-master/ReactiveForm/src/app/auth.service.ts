import { Injectable } from '@angular/core';
import { CanActivate } from '../../node_modules/@angular/router';
import { Router } from '@angular/router'
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements CanActivate {

  constructor(private http:HttpClient, private router:Router) { }

  canActivate()
  {
    if(localStorage.getItem('user')==='valid' )
    {
      return true;
    }
    else
    {
      false;
      this.router.navigate(['/loginPage']);
    }
  }
}