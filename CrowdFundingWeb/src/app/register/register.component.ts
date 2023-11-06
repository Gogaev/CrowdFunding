import { Component } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent{
  form:FormGroup;

  constructor(private fb:FormBuilder, private accountService: AccountService, private router: Router){
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
      this.accountService.register(val.username, val.fullName, val.description, val.emailAddress, val.password, val.adminKey, val.role)
          .subscribe(
              () => {
                  console.log("User is registered in");
                  this.router.navigateByUrl('/');
              }
          );
    }
  }
}
