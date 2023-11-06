import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'CrowdFundingWeb';
  projects: any;
  
  constructor(private http: HttpClient){}
  
  ngOnInit(): void {
    this.http.get('http://localhost:5185/api/Projects/get-published', { withCredentials: true }).subscribe({
      next: response => this.projects = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed') 
    });
  }
}
