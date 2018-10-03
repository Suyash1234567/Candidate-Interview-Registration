import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ServicefirstService {
isLogin:boolean=false
userData:any
data
public mydata= new Subject<any>()
public user = new Subject<any>()
  constructor() { }
  getUserData(result){
    
    this.user.next(result);
  }
  getLogin(result){
    
    this.mydata.next(result);
  }

}
