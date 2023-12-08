import { DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { IProjectWithTiersDto } from 'src/app/Scripts/Core/Dtos/Project/IProjectWithTiersDto';
import { ProjectsApiService } from 'src/app/Scripts/CrowdFundingAPI/Controllers/ProjectsController';
import { IUpdateProjectCommand } from 'src/app/Scripts/Domain/Features/ProjectFeatures/Commands/IUpdateProjectCommand';
import { ImageService } from 'src/app/_services/image.service';

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.css']
})
export class EditProjectComponent {
  projectId: string;
  project : IProjectWithTiersDto;
  form: FormGroup;
  fileToUpload: File | null = null;
  commandUpdate: IUpdateProjectCommand = {
    id: '',
    description: '',
    imageUrl: '',
    lastDay: new Date(),
    requiredMoney: 0,
    title: ''
  };

  constructor(private route: ActivatedRoute,
    private datePipe: DatePipe,
     private projectService : ProjectsApiService,
     private imageService: ImageService,
     private sanitizer: DomSanitizer,
     private router: Router,
     private fb:FormBuilder) {this.form = this.fb.group({
      title: ['',Validators.required],
      description: ['',Validators.required],
      imageUrl: ['',Validators.required],
      lastDay: ['',Validators.required],
      requiredMoney: ['',Validators.required],
    });}

  ngOnInit() {
      this.route.params.subscribe(params => {
      this.projectId = params['id'];
    });
    this.projectService.getById(this.projectId).subscribe(response => {
      this.project = response;
      this.imageService.getFile(response.imageUrl).subscribe(i=>
        {
          this.fileToUpload = i
          this.image = i
          this.imageURL = this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(this.image))
        })
        const formattedDate = this.datePipe.transform(this.project.lastDay, 'yyyy-MM-dd');
        this.form.patchValue({
          title: this.project.title,
          requiredMoney: this.project.requiredMoney,
          description: this.project.description,
          lastDay: formattedDate
        });
    });
  }

  image:Blob
  imageURL:SafeUrl

  handleFileInput(event: any) {

    const files = event?.target?.files; // Перевірка на існування об'єкту files
    if (files && files.length > 0) {
      this.fileToUpload = files.item(0);
      console.log('File added');
    }
  }

  updateeProject(){
    if (this.fileToUpload) {
      this.imageService.uploadFile(this.fileToUpload).subscribe((response) => {
        const val = this.form.value;
        if(val){
        this.commandUpdate.id = this.projectId;
        this.commandUpdate.title = val.title;
        this.commandUpdate.description = val.description;
        this.commandUpdate.lastDay = new Date(val.lastDay);
        this.commandUpdate.lastDay.toISOString();
        this.commandUpdate.imageUrl = response;
        this.commandUpdate.requiredMoney = val.requiredMoney;
        this.projectService.update(this.commandUpdate).subscribe(
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
