import { SafeUrl } from "@angular/platform-browser";
import { IPublishedProjectDto } from "../Scripts/Core/Dtos/Project/IPublishedProjectDto";

export interface ProjectWithImage{
    project: IPublishedProjectDto;
    image:Blob;
    imageURL:SafeUrl;
}