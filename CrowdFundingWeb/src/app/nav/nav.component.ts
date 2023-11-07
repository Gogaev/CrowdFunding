import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { TokenStorageService } from '../_services/_tokenServices/token-storage.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  loggedIn = false
  
  constructor(public accountService: AccountService, private router: Router, public tokenStorageService: TokenStorageService){}
  
  ngOnInit(): void {
  }
  
  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
