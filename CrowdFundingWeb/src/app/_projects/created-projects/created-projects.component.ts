import { Component, OnInit } from '@angular/core';
import { ProjectsApiService } from 'src/app/Scripts/CrowdFundingAPI/Controllers/ProjectsController';

@Component({
  selector: 'app-created-projects',
  templateUrl: './created-projects.component.html',
  styleUrls: ['./created-projects.component.css']
})
export class CreatedProjectsComponent implements OnInit {
  title = 'CrowdFundingWeb';
  projects: any;
  
  constructor(private projectService: ProjectsApiService){}
  
  ngOnInit(): void {
    this.projectService.getAllByUser().subscribe({
      next: response => this.projects = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed') 
    });
  }
}
