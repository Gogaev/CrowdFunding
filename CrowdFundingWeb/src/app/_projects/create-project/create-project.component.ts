import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProjectsApiService } from 'src/app/Scripts/CrowdFundingAPI/Controllers/ProjectsController';
import { ICreateProjectCommand } from 'src/app/Scripts/Domain/Features/ProjectFeatures/Commands/ICreateProjectCommand';
import { ImageService } from 'src/app/_services/image.service';

@Component({
  selector: 'app-create-project',
  templateUrl: './create-project.component.html',
  styleUrls: ['./create-project.component.css']
})
export class CreateProjectComponent {
  form: FormGroup;
  fileToUpload: File | null = null;
  commandCreate: ICreateProjectCommand = {
    description: '',
    imageUrl: '',
    lastDay: new Date(),
    requiredMoney: 0,
    title: ''
  }

  constructor(private fb:FormBuilder,
     private projectService: ProjectsApiService,
     private router: Router,
     private image: ImageService){
    this.form = this.fb.group({
      title: ['',Validators.required],
      description: ['',Validators.required],
      imageUrl: ['',Validators.required],
      lastDay: ['',Validators.required],
      requiredMoney: ['',Validators.required],
    });
  }

  handleFileInput(event: any) {

    const files = event?.target?.files; // Перевірка на існування об'єкту files
    if (files && files.length > 0) {
      this.fileToUpload = files.item(0);
      console.log('File added');
    }
  }

  createProject(){
    if (this.fileToUpload) {
      this.image.uploadFile(this.fileToUpload).subscribe((response) => {
        const val = this.form.value;
        if(val){
        this.commandCreate.title = val.title;
        this.commandCreate.description = val.description;
        this.commandCreate.lastDay = new Date(val.lastDay);
        this.commandCreate.lastDay.toISOString();
        this.commandCreate.imageUrl = response;
        this.commandCreate.requiredMoney = val.requiredMoney;
        this.projectService.create(this.commandCreate).subscribe(
        (result) => {
          console.log(result);
          this.router.navigateByUrl('my-projects');
      }
    );
    }
      });
    }
    
  }

}
