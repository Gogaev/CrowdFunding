import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  form:FormGroup;

    constructor(private fb:FormBuilder, 
                 private authService: AccountService, 
                 private router: Router,
                 private toastr: ToastrService) {

        this.form = this.fb.group({
          username: ['',Validators.required],
            password: ['',Validators.required]
        });
    }
    

    login() {
        const val = this.form.value;

        if (val.username && val.password) {
            this.authService.login(val.username, val.password)
                .subscribe(
                    () => {
                        this.toastr.success("You are logged in now")
                        this.router.navigateByUrl('/');
                    }
                );

        }
    }
}
