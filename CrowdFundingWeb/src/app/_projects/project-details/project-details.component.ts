import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProjectWithTiersDto } from 'src/app/Scripts/Core/Dtos/Project/IProjectWithTiersDto';
import { ProjectsApiService } from 'src/app/Scripts/CrowdFundingAPI/Controllers/ProjectsController';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css']
})
export class ProjectDetailsComponent {
  projectId: string = "";
  project : IProjectWithTiersDto = {
    id: '',
    creatorName: '',
    description: '',
    imageUrl: '',
    investedMoney: 0,
    lastDay: new Date,
    requiredMoney: 0,
    title: '',
    status:'',
    tiers: []
  };

  constructor(private route: ActivatedRoute, private projectService : ProjectsApiService) {}

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.projectId = params['id'];
    });
    this.projectService.getById(this.projectId).subscribe(response => {
      this.project = response;
    });
  }
}
