import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})


export class NavComponent {
  currentUser$: Observable<User | null> = of(null)
  model: any = {}

  constructor(public accountService: AccountService,private router: Router,private toastr :ToastrService) { }
  ngOnInit(): void {
    // this.currentUser$ = this.accountService.currentUser$;
  }


  login() {
    this.accountService.login(this.model).subscribe({
      next: () => this.router.navigateByUrl('/members'),
      error: error => this.toastr.error(error.error) //console.log(error)



    })
  }

  logout() {

    this.accountService.logout();
    this.router.navigateByUrl('/')
  }

}
