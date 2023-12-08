import { createFeatureSelector, createSelector } from "@ngrx/store";
import { ProjectState, projectsAdapter } from "./state";

const projectsFeature = createFeatureSelector<ProjectState>(
    'projects',
  );
  
  export const selectors = projectsAdapter.getSelectors();
  
  export const selectAllProjects = createSelector(
    projectsFeature,
    selectors.selectAll
  );