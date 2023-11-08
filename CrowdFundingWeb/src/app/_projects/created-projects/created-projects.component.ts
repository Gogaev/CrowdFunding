import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-created-projects',
  templateUrl: './created-projects.component.html',
  styleUrls: ['./created-projects.component.css']
})
export class CreatedProjectsComponent implements OnInit {
  title = 'CrowdFundingWeb';
  projects: any;
  
  constructor(private http: HttpClient){}
  
  ngOnInit(): void {
    this.http.get(environment.projectsEndpointUrl + 'get-created').subscribe({
      next: response => this.projects = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed') 
    });
  }
}
