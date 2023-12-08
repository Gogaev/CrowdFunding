import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { ProjectWithImage } from "../_models/project-image";

export const ProjectsActions = createActionGroup({
    source: 'Projects',
    events: {
      'Load All': emptyProps(),
      'Load All Success': props<{projects: ProjectWithImage[]}>(),
    },
  });