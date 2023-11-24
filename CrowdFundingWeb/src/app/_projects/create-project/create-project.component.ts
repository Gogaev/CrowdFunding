import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProjectsApiService } from 'src/app/Scripts/CrowdFundingAPI/Controllers/ProjectsController';
import { ICreateProjectCommand } from 'src/app/Scripts/Domain/Features/ProjectFeatures/Commands/ICreateProjectCommand';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent {
  form: FormGroup;
  commandCreate: ICreateProjectCommand = {
    description: '',
    imageUrl: '',
    lastDay: new Date(),
    requiredMoney: 0,
    title: ''
  }

  constructor(private fb:FormBuilder, private projectService: ProjectsApiService, private router: Router){
    this.form = this.fb.group({
      title: ['',Validators.required],
      description: ['',Validators.required],
      imageUrl: ['',Validators.required],
      lastDay: ['',Validators.required],
      requiredMoney: ['',Validators.required],
    });
  }

  createProject(){
    const val = this.form.value;
    if(val){
      this.commandCreate.title = val.title;
      this.commandCreate.description = val.description;
      this.commandCreate.imageUrl = val.imageUrl;
      this.commandCreate.lastDay = new Date(val.lastDay);
      this.commandCreate.lastDay.toISOString();
      this.commandCreate.requiredMoney = val.requiredMoney;
      console.log(this.commandCreate.lastDay)
      this.projectService.create(this.commandCreate).subscribe(
      (result) => {
          console.log(result);
          this.router.navigateByUrl('my-projects');
      }
    );
    }
  }

}
