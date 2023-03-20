import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.css']
})
export class TestErrorComponent {
baseUrl="https://localhost:5001/api/";

validationErrormsgs:string[]=[];
user:User
/**
 *
 */
| undefined

/**
 *
 */
constructor(private http:HttpClient,private toastr:ToastrService) {}

get404Error(){
  this.http.get(this.baseUrl+'buggy/not-found').subscribe({
    next: response => console.log(response),
    error:error=>console.log(error.error)
  })
}

get400Error(){
  this.http.get(this.baseUrl+'buggy/bad-request').subscribe({
    next: response => console.log(response),
    error:error=>console.log(error.error)
  })
}

get500Error(){
  this.http.get(this.baseUrl+'buggy/server-error').subscribe({
    next: response => console.log(response),
    error:error=>console.log(error.error)
  })
}

get401Error(){
  this.http.get(this.baseUrl+'buggy/auth').subscribe({
    next: response => console.log(response),
    error:error=>console.log(error.error)
  })
}

get400RegisterError(){
  
  this.http.post(this.baseUrl+'account/register',this.user).subscribe({
    next: response => console.log(response),
    error:error=>{
      console.log(error.error);
      this.validationErrormsgs=error.error;

    }
  })
}



}
