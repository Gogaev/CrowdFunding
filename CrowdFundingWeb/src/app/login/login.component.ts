import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { ILoginUserCommand } from '../Scripts/Domain/Features/UserFeatures/Commands/ILoginUserCommand';
import { UserApiService } from '../Scripts/CrowdFundingAPI/Controllers/UserController';
import { TokenStorageService } from '../_services/_tokenServices/token-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form:FormGroup;
  loginCommand: ILoginUserCommand = {
    username: '',
    password:''
  }

    constructor(private fb:FormBuilder, 
                 private userService: UserApiService, 
                 private router: Router,
                 private toastr: ToastrService,
                 private tokenStorageService: TokenStorageService) {

        this.form = this.fb.group({
          username: ['',Validators.required],
            password: ['',Validators.required]
        });
    }
    

    login() {
        const val = this.form.value;

        if (val.username && val.password) {
          this.loginCommand.username = val.username;
          this.loginCommand.password = val.password;
            this.userService.login(this.loginCommand)
                .subscribe(
                    (result) => {
                      this.tokenStorageService.setToken(result.token)
                        this.toastr.success("You are logged in now")
                        this.router.navigateByUrl('/');
                    }
                );

        }
    }
}
