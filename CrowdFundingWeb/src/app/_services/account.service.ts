import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { BehaviorSubject, map } from 'rxjs';
import { TokenStorageService } from './_tokenServices/token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'http://localhost:5185/api/';
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private tokenStorageService: TokenStorageService) { }

  login(username:string, password:string){
    // return this.http.post<User>(this.baseUrl + 'user/login', {username, password}); 
    return this.http.post<User>(this.baseUrl + 'user/login', {username, password}).pipe(
      map((response: User) => {
        const user = response;
        if(user){
          this.tokenStorageService.setToken(JSON.stringify(user.token));
          this.currentUserSource.next(user);
        }
      })
    ); 
  }

  logout(){
    this.tokenStorageService.removeToken();
    this.currentUserSource.next(null);
  }

  register(username:string, fullName:string, description:string, emailAddress:string, password:string, adminKey:string,  role:string){
    return this.http.post<User>(this.baseUrl + 'user/register', {username, fullName, description, emailAddress, password, adminKey, role}).pipe(
      map(user => {
        if(user){
          this.tokenStorageService.setToken(JSON.stringify(user.token));
          this.currentUserSource.next(user);
        }
      })
    )
  }
}