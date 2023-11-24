import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserApiService } from '../Scripts/CrowdFundingAPI/Controllers/UserController';
import { IRegisterUserCommand } from '../Scripts/Domain/Features/UserFeatures/Commands/IRegisterUserCommand';
import { TokenStorageService } from '../_services/_tokenServices/token-storage.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent{
  form:FormGroup;
  commandRegister: IRegisterUserCommand = {
    userName: '',
    adminKey: '',
    description: '',
    emailAddress: '',
    fullName: '',
    password: '',
    role: ''
  }

  constructor(private fb:FormBuilder, private userService: UserApiService ,private tokenStorageService: TokenStorageService, private router: Router){
    this.form = this.fb.group({
      username: ['',Validators.required],
      fullName: ['',Validators.required],
      description: ['',Validators.required],
      emailAddress: ['',Validators.required],
      password: ['',Validators.required],
      adminKey: ['',Validators.required],
      role: ['',Validators.required]
    });
  }

  

  register(){ 
    const val = this.form.value;
    if (val.username && val.password) {
      this.commandRegister.description = val.description;
      this.commandRegister.userName = val.username;
      this.commandRegister.fullName = val.fullName;
      this.commandRegister.emailAddress = val.emailAddress;
      this.commandRegister.password = val.password;
      this.commandRegister.description = val.description;
      this.commandRegister.description = val.description;
      this.userService.register(this.commandRegister)
          .subscribe(
              (result) => {
                  this.tokenStorageService.setToken(result.token);
                  console.log("User is registered in");
                  this.router.navigateByUrl('/');
              }
          );
    }
  }
}
