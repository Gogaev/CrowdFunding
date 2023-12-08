import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IProjectWithTiersDto } from 'src/app/Scripts/Core/Dtos/Project/IProjectWithTiersDto';
import { ProjectsApiService } from 'src/app/Scripts/CrowdFundingAPI/Controllers/ProjectsController';
import { TiersApiService } from 'src/app/Scripts/CrowdFundingAPI/Controllers/TiersController';
import { IPublishProjectCommand } from 'src/app/Scripts/Domain/Features/ProjectFeatures/Commands/IPublishProjectCommand';
import { IGetAllCreatedProjectsQuery } from 'src/app/Scripts/Domain/Features/ProjectFeatures/Queries/IGetAllCreatedProjectsQuery';
import { ICreateTierCommand } from 'src/app/Scripts/Domain/Features/TierFeature/Commands/ICreateTierCommand';

@Component({
  selector: 'app-created-projects',
  templateUrl: './created-projects.component.html',
  styleUrls: ['./created-projects.component.css']
})
export class CreatedProjectsComponent implements OnInit {
  title = 'CrowdFundingWeb';
  form: FormGroup;
  selectedOption: number = 4;
  projects: IProjectWithTiersDto[] = [];
  isOpened: boolean[] = [];
  commandGetCreated: IGetAllCreatedProjectsQuery= {
    status: 4
  }
  commandPublish: IPublishProjectCommand = {
    id: ''
  }
  commandCreateTier: ICreateTierCommand = {
    benefit: '',
    projectId: '',
    requiredMoney: 0,
    tierName: ''
  };
  
  constructor(private projectService: ProjectsApiService,
     private router: Router,
     private fb:FormBuilder,
     private tierService: TiersApiService){
    this.isOpened = new Array(this.projects.length).fill(true);
    this.form = this.fb.group({
      tierName: ['',Validators.required],
      benefit: ['',Validators.required],
      requiredMoney: ['',Validators.required],
    });
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

  goToEditProject(id: string){
    this.router.navigate(['/edit-project', id]);
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
    this.projectService.softDelete(id).subscribe(
      (result) => {
          console.log(result);
          this.router.navigateByUrl('/');
      }
    );
  }

  createTier(id: string){
    const val = this.form.value;
    this.commandCreateTier.benefit = val.benefit;
    this.commandCreateTier.requiredMoney = val.requiredMoney;
    this.commandCreateTier.tierName = val.tierName;
    this.commandCreateTier.projectId = id;
    this.tierService.create(this.commandCreateTier).subscribe(
      (result) => {
        console.log(result);
        this.router.navigateByUrl('my-projects');
    }
  );
  }

  deleteTier(id: string){
    this.tierService.delete(id).subscribe(
      (result) => {
          console.log(result);
          this.router.navigateByUrl('/');
      }
    );
  }
}
