import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { User } from '../_models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any;

  ngOnInit(): void {
    this.users = this.getUsers();
  }
  /**
   *
   */
  constructor(private http: HttpClient) {
  }



  IsToggled() {
    this.registerMode = !this.registerMode;
  }
 
  getUsers() {
    this.http.get('https://localhost:5001/api/user').subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log('Request has been completed.')
    })
  }

  cancelRegisterMode(event :boolean)
  {
    this.registerMode=event;
  }

}


