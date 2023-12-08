import { Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ImageService } from '../image.service';
import { ProjectsApiService } from 'src/app/Scripts/CrowdFundingAPI/Controllers/ProjectsController';
import { Observable, map, forkJoin, switchMap } from 'rxjs';
import { ProjectWithImage } from 'src/app/_models/project-image';
import { IPublishedProjectDto } from 'src/app/Scripts/Core/Dtos/Project/IPublishedProjectDto';

@Injectable({
  providedIn: 'root'
})
export class GetProjetsService {

  constructor(private projectService: ProjectsApiService,
    private imageService: ImageService,
    private sanitizer: DomSanitizer)
    {}

    getProjectsWithImages(): Observable<ProjectWithImage[]> {
      return this.projectService.getAllPublished().pipe(
        switchMap((projects: IPublishedProjectDto[]) => {
          const requests: Observable<Blob>[] = [];
          projects.forEach((project: IPublishedProjectDto) => {
            requests.push(this.imageService.getFile(project.imageUrl));
          });
  
          return forkJoin(requests).pipe(
            map((images: Blob[]) => {
              const projectsWithImages: ProjectWithImage[] = [];
              images.forEach((image: Blob, index: number) => {
                const projectWithImage: ProjectWithImage = {
                  project: projects[index],
                  image: image,
                  imageURL: this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(image))
                };
                projectsWithImages.push(projectWithImage);
              });
              return projectsWithImages;
            })
          );
        })
      );
    }
}
