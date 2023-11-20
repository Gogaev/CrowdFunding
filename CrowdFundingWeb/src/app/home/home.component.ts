import { Component, OnInit } from '@angular/core';
import { TokenStorageService } from '../_services/_tokenServices/token-storage.service';
import { Router } from '@angular/router';
import { ProjectsApiService } from '../Scripts/CrowdFundingAPI/Controllers/ProjectsController';
import { IPublishedProjectDto } from '../Scripts/Core/Dtos/Project/IPublishedProjectDto';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  title = 'CrowdFundingWeb';
  projects: IPublishedProjectDto[] = [];
  
  constructor(private projectService: ProjectsApiService,
     public tokenStorageService: TokenStorageService,
      private router: Router){}
  
  ngOnInit(): void {
    this.projectService.getAllPublished().subscribe({
      next: response => this.projects = Object.values(response),
      error: error => console.log(error),
      complete: () => console.log('Request has completed') 
    }
    );
  }
  goToProjectDetails(id: string) {
    this.router.navigate(['/project-details', id]);
  }

  goToProjectSupport(id: string) {
    this.router.navigate(['/project-support', id]);
  }
}
