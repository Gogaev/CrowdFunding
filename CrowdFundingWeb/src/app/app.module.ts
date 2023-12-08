import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { TokenInterceptorService } from './_services/_tokenServices/token-interceptor.service';
import { AuthInterceptorService } from './_services/_tokenServices/auth-interceptor.service';
import { CreatedProjectsComponent } from './_projects/created-projects/created-projects.component';
import { ToastrModule } from 'ngx-toastr';
import { ProjectDetailsComponent } from './_projects/project-details/project-details.component';
import { SupportProjectComponent } from './_projects/support-project/support-project.component';
import { TimerComponent } from './timer/timer.component';
import { CreateProjectComponent } from './_projects/create-project/create-project.component';
import { MatStepperModule } from '@angular/material/stepper';
import { MatIconModule } from '@angular/material/icon';
import { EditProjectComponent } from './_projects/edit-project/edit-project.component';
import { DatePipe } from '@angular/common';
import { ErrorInterceptor } from './_services/_exceptionServices/error.interceptor';
import { NotFoundComponent } from './_errors/not-found/not-found.component';
import { ServerErrorComponent } from './_errors/server-error/server-error.component';
import { StoreModule } from '@ngrx/store';
import { SupportedProjectsComponent } from './_projects/supported-projects/supported-projects.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    CreatedProjectsComponent,
    ProjectDetailsComponent,
    SupportProjectComponent,
    TimerComponent,
    CreateProjectComponent,
    EditProjectComponent,
    NotFoundComponent,
    ServerErrorComponent,
    SupportedProjectsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    BsDropdownModule.forRoot(),
    ReactiveFormsModule,
    MatStepperModule,
    MatIconModule,
    ToastrModule.forRoot(),
    StoreModule.forRoot({}, {})
  ],
  providers: [
    DatePipe,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
