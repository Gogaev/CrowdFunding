import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IProjectWithTiersDto } from 'src/app/Scripts/Core/Dtos/Project/IProjectWithTiersDto';
import { ProjectsApiService } from 'src/app/Scripts/CrowdFundingAPI/Controllers/ProjectsController';
import { IPublishProjectCommand } from 'src/app/Scripts/Domain/Features/ProjectFeatures/Commands/IPublishProjectCommand';
import { IGetAllCreatedProjectsQuery } from 'src/app/Scripts/Domain/Features/ProjectFeatures/Queries/IGetAllCreatedProjectsQuery';

@Component({
  selector: 'app-created-projects',
  templateUrl: './created-projects.component.html',
  styleUrls: ['./created-projects.component.css']
})
export class CreatedProjectsComponent implements OnInit {
  title = 'CrowdFundingWeb';
  selectedOption: number = 4;
  projects: IProjectWithTiersDto[] = [];
  isOpened: boolean[] = [];
  commandGetCreated: IGetAllCreatedProjectsQuery= {
    status: 4
  }
  commandPublish: IPublishProjectCommand = {
    id: ''
  }
  
  constructor(private projectService: ProjectsApiService, private router: Router){
    this.isOpened = new Array(this.projects.length).fill(true);
  }
  
  ngOnInit(): void {
    this.projectService.getAllByUser(4).subscribe({
      next: response => this.projects = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed') 
    });
    console.log(this.commandGetCreated);
  }

  toggleCollapse(index: number) {
    this.isOpened[index] = !this.isOpened[index];
  }

  getStatusColor(status: string): string {
    switch (status) {
      case 'Draft':
        return 'bg-light';
      case 'Expired':
        return 'bg-dark';
      case 'Published':
        return 'bg-primary';
      case 'Finished':
        return 'bg-success';
      default:
        return 'dark';
    }
  }

  publishItem(id: string) {
    this.commandPublish.id = id
    this.projectService.publishProject(this.commandPublish).subscribe(
      (result) => {
          console.log(result);
          this.router.navigateByUrl('/');
      }
    );
  }

  goToProjectDetails(id: string) {
    this.router.navigate(['/project-details', id]);
  }

  goToCreateProject(){
    this.router.navigate(['/create-project']);
  }

  applyFilter(){
    this.projectService.getAllByUser(this.selectedOption).subscribe({
      next: response => this.projects = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed') 
    });
    console.log(this.commandGetCreated);
  }

  deleteProject(id: string){
    this.projectService.delete(id).subscribe(
      (result) => {
          console.log(result);
          this.router.navigateByUrl('/');
      }
    );
  }
}
