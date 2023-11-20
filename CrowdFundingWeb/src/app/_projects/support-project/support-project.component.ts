import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IProjectWithTiersDto } from 'src/app/Scripts/Core/Dtos/Project/IProjectWithTiersDto';
import { ProjectsApiService } from 'src/app/Scripts/CrowdFundingAPI/Controllers/ProjectsController';
import { ISupportProjectCommand } from 'src/app/Scripts/Domain/Features/ProjectFeatures/Commands/ISupportProjectCommand';

@Component({
  selector: 'app-support-project',
  templateUrl: './support-project.component.html',
  styleUrls: ['./support-project.component.css']
})
export class SupportProjectComponent {
  projectId: string = '';
  supportCommand: ISupportProjectCommand = {
     projectId: this.projectId,
     moneyAmount: 0,
  }
  project: IProjectWithTiersDto = {
    id: '',
    title: '',
    description: '',
    imageUrl: '',
    creatorName: '',
    requiredMoney: 0,
    investedMoney: 0,
    lastDay: new Date(),
    tiers: []
  };
  form:FormGroup;

  constructor(private route: ActivatedRoute,
     private projectService : ProjectsApiService,
     private router: Router,
     private toastr: ToastrService,
     private fb:FormBuilder) {
      this.form = this.fb.group({
        moneyAmount: ['',Validators.required]
      });
     }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.projectId = params['id'];
    });
    this.projectService.getById(this.projectId).subscribe(response => {
      this.project = response;
    });
  }

  support(money: number){
    this.supportCommand.moneyAmount = money; 
    this.supportCommand.projectId = this.projectId; 
    this.projectService.supportProject(this.supportCommand).subscribe(response => {
      console.log(response);
    });
    this.router.navigateByUrl('/');
    this.toastr.success("Thank for your support");
  }

  supportForm(){
    const val = this.form.value;
    this.supportCommand.moneyAmount = val.moneyAmount; 
    this.supportCommand.projectId = this.projectId; 
    if (val.moneyAmount) {
      this.projectService.supportProject(this.supportCommand).subscribe(response => {
        console.log(response);
      });
      this.router.navigateByUrl('/');
      this.toastr.success("Thank for your support");
    }
  }
}
