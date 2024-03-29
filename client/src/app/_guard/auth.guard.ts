import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  /**
   *
   */
  constructor(private accountservice:AccountService, private toastr:ToastrService) {  }
  canActivate(): Observable<boolean> {
    return this.accountservice.currentUser$.pipe(
      map(user=>{
        if(user) return true
        else{
          this.toastr.error('Please login into your account.')
          return false;
        }
      })
    )
  }
  
}
