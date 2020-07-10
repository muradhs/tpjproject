import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode =false; 
  userCreatedMode = false; 
  
  constructor() { }

  ngOnInit() {
  }
  cancelRegister(cancelRegister : boolean){
 
   this.registerMode =cancelRegister; 
   this.userCreatedMode = false;
  }
  userCreated(userCreated : boolean){
   this.userCreatedMode = userCreated; 
  }
  toggleMode(){

    this.registerMode = !this.registerMode; 
      console.log('Clicked ' + this.registerMode ); 
      this.userCreatedMode = false;
  }
  loggedIn(){
    const token = localStorage.getItem('token'); 
      return !!token ; // if theere is a token it will return true 
   
  }
}
