import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { IProjectWithTiersDto } from 'src/app/Scripts/Core/Dtos/Project/IProjectWithTiersDto';
import { ProjectsApiService } from 'src/app/Scripts/CrowdFundingAPI/Controllers/ProjectsController';
import { ImageService } from 'src/app/_services/image.service';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css']
})
export class ProjectDetailsComponent implements OnInit{
  projectId: string;
  project : IProjectWithTiersDto;

  constructor(private route: ActivatedRoute,
     private projectService : ProjectsApiService,
     private imageService: ImageService,
     private sanitizer: DomSanitizer,
     private router: Router) {}

  ngOnInit() {
      this.route.params.subscribe(params => {
      this.projectId = params['id'];
    });
    this.projectService.getById(this.projectId).subscribe(response => {
      this.project = response;
      this.imageService.getFile(response.imageUrl).subscribe(i=>
        {
          this.image = i
          this.imageURL = this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(this.image))
        })
    });
  }

  goToProjectSupport(id: string) {
    this.router.navigate(['/project-support', id]);
  }

  image:Blob
  imageURL:SafeUrl
}
