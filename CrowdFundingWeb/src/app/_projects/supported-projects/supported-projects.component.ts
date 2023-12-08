import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IProjectDto } from 'src/app/Scripts/Core/Dtos/Project/IProjectDto';
import { ProjectsApiService } from 'src/app/Scripts/CrowdFundingAPI/Controllers/ProjectsController';

@Component({
  selector: 'app-supported-projects',
  templateUrl: './supported-projects.component.html',
  styleUrls: ['./supported-projects.component.css']
})
export class SupportedProjectsComponent implements OnInit {
  projects: IProjectDto[] = []


  constructor(private projectService: ProjectsApiService, private router: Router){}
  
  ngOnInit(): void {
    this.projectService.getSupported().subscribe(
      (projects: IProjectDto[])=>{
        this.projects = projects;
      },
      (error: any) => {
        console.error('An error occurred while fetching projects with images:', error);
      }
    )
  }

  goToProjectDetails(id: string) {
    this.router.navigate(['/project-details', id]);
  }

}
