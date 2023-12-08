import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { CreatedProjectsComponent } from './_projects/created-projects/created-projects.component';
import { ProjectDetailsComponent } from './_projects/project-details/project-details.component';
import { SupportProjectComponent } from './_projects/support-project/support-project.component';
import { CreateProjectComponent } from './_projects/create-project/create-project.component';
import { EditProjectComponent } from './_projects/edit-project/edit-project.component';
import { NotFoundComponent } from './_errors/not-found/not-found.component';
import { ServerErrorComponent } from './_errors/server-error/server-error.component';
import { SupportedProjectsComponent } from './_projects/supported-projects/supported-projects.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'my-projects', component: CreatedProjectsComponent},
  {path: 'project-details/:id', component: ProjectDetailsComponent},
  {path: 'project-support/:id', component: SupportProjectComponent},
  {path: 'create-project', component: CreateProjectComponent},
  {path: 'edit-project/:id', component: EditProjectComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: 'supported', component: SupportedProjectsComponent},
  {path: '**', component: NotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
