import { Component, OnInit } from '@angular/core';
import { TokenStorageService } from '../_services/_tokenServices/token-storage.service';
import { Router } from '@angular/router';
import { ProjectWithImage } from '../_models/project-image';
import { GetProjetsService } from '../_services/projectServices/get-projets.service';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';
import { selectAllProjects } from'../_state-management/selectors'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  title = 'CrowdFundingWeb';
  projectsWithImages: ProjectWithImage[] = [];
  projects$ = Observable<ProjectWithImage[]>;
  
  constructor(
     public tokenStorageService: TokenStorageService,
     private projectImageService: GetProjetsService,
     private readonly store: Store,
     private router: Router){}
  
  ngOnInit(): void {
    //this.projects$ = this.store.pipe(select(selectAllProjects));
    this.loadProjectsWithImages();
  }
  
  loadProjectsWithImages(): void {
    this.projectImageService.getProjectsWithImages().subscribe(
      (projectsWithImages: ProjectWithImage[]) => {
        this.projectsWithImages = projectsWithImages;
      },
      (error: any) => {
        console.error('An error occurred while fetching projects with images:', error);
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

