import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { CreatedProjectsComponent } from './_projects/created-projects/created-projects.component';
import { ProjectDetailsComponent } from './_projects/project-details/project-details.component';
import { SupportProjectComponent } from './_projects/support-project/support-project.component';
import { CreateProjectComponent } from './_projects/create-project/create-project.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'my-projects', component: CreatedProjectsComponent},
  {path: 'project-details/:id', component: ProjectDetailsComponent},
  {path: 'project-support/:id', component: SupportProjectComponent},
  {path: 'create-project', component: CreateProjectComponent},
  {path: '**', component: HomeComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
