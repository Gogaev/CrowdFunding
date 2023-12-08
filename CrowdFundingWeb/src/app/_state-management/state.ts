import { EntityState, createEntityAdapter } from "@ngrx/entity";
import { ProjectWithImage } from "../_models/project-image";

export interface ProjectState extends EntityState<ProjectWithImage> {}
export const projectsAdapter = createEntityAdapter<ProjectWithImage>();