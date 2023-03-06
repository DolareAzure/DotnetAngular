import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model:any={}
  user: User | undefined;
  
 
  @Output() cancelRegister=new EventEmitter;

  /**
   *
   */
  constructor(private accountservice:AccountService,private http:HttpClient) {
    
  }

  ngOnInit():void{

  }

  register()
  {
    
    this.accountservice.register(this.model).subscribe({
      next: response=>{
        console.log(response);
        this.cancel();
      },
      error: error=> console.log(error)

    })
    
  }

  cancel()
  {
    this.cancelRegister.emit(false);
  }

}
