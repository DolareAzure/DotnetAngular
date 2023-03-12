import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
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
  constructor(private accountservice:AccountService,private http:HttpClient,private toastr:ToastrService) {
    
  }

  ngOnInit():void{

  }

  register()
  {
    
    this.accountservice.register(this.model).subscribe({
      next: response=>{
        console.log(response);
        this.toastr.success(this.model.username+' registered successfully')
        this.cancel();
      },
      error: error=> this.toastr.error(error.error)

    })
    
  }

  cancel()
  {
    this.cancelRegister.emit(false);
  }

}
