import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
model: any = {};
user : any ;  ; 
  constructor(private authService: AuthService ) { }

  ngOnInit() {
  }

 login(){
  this.authService.login(this.model).subscribe(next => {
  this.user =localStorage.getItem('user').replace(/^./, str => str.toUpperCase()),
      error =>{
          console.log("Log in failed  ")
            }

    });

 }

 loggedIn(){
 
 const token = localStorage.getItem('token'); 
   return !!token ; // if theere is a token it will return true 

 }
 logOut(){
   localStorage.removeItem('token'); 
   localStorage.removeItem('user'); 
   console.log('User logged out'); 
 }
}
