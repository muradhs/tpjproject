import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 @Output()  cancelRegister = new EventEmitter(); 
 @Output()  userCreated = new EventEmitter(); 
  model: any =  {}; 
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }
  cancel(){
    this.cancelRegister.emit(false); 
  }
  register(){
    this.authService.register(this.model).subscribe(next => {
      this.userCreated.emit(true),
      error =>{
        this.userCreated.emit(false)
            }

    });
  }
  
}
