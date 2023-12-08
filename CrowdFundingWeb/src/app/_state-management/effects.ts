import { Actions, createEffect, ofType } from "@ngrx/effects";
import { mergeMap, map } from "rxjs";
import { ProjectsActions } from "./action";
import { GetProjetsService } from "../_services/projectServices/get-projets.service";
import { Injectable } from "@angular/core";

@Injectable()
export class ProjectsEffects{

    constructor(private actions$: Actions, private getProjectService: GetProjetsService) {}

    loadProjects$ = createEffect(() => this.actions$.pipe(
        ofType(ProjectsActions.loadAll),
        mergeMap(() => this.getProjectService.getProjectsWithImages()
          .pipe(
            map(projects => ProjectsActions.loadAllSuccess({ projects }))
          ))
        )
      );
}