import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  title = 'CrowdFundingWeb';
  projects: any;
  
  constructor(private http: HttpClient){}
  
  ngOnInit(): void {
    this.http.get(environment.projectsEndpointUrl + 'get-published').subscribe({
      next: response => this.projects = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed') 
    });
  }
}
