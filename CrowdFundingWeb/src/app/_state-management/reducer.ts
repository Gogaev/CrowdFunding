import { createReducer, on } from "@ngrx/store";
import { ProjectsActions } from "./action";
import { projectsAdapter } from "./state";

export const projectsReducer = createReducer(
    projectsAdapter.getInitialState(),
    on(ProjectsActions.loadAllSuccess, (state, { projects }) =>
        projectsAdapter.addMany(projects, state)
    ),
  );