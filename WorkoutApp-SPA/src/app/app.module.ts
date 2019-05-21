import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule, ButtonsModule } from 'ngx-bootstrap';
import { appRoutes } from './routes';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { RouterModule } from '@angular/router';
import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { UserService } from './_services/user.service';
import { AuthGuard } from './_guards/auth.guard';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { PlanOverviewResolver } from './_resolvers/planOverview.resolver';
import { RegisterComponent } from './register/register.component';
import { WorkoutCardComponent } from './workout-card/workout-card.component';
import { PlanOverviewComponent } from './plan-overview/plan-overview.component';
import { WorkoutCalendarComponent } from './workout-calendar/workout-calendar.component';
import { WorkoutDayComponent } from './workout-day/workout-day.component';
import { WorkoutDetailsComponent } from './workout-details/workout-details.component';
import { WorkoutDetailsResolver } from './_resolvers/workout-details.resolver';
import { ExerciseDetailsComponent } from './exercise-details/exercise-details.component';
import { ExerciseService } from './_services/exercise.service';
import { ExerciseDetailsResolver } from './_resolvers/exercise-details.resolver';
import { WorkoutPlanService } from './_services/workout-plan.service';

export function tokenGetter() {
    return localStorage.getItem('token');
}

@NgModule({
    declarations: [
        AppComponent,
        NavComponent,
        HomeComponent,
        RegisterComponent,
        PlanOverviewComponent,
        WorkoutCardComponent,
        WorkoutCalendarComponent,
        WorkoutDayComponent,
        WorkoutDetailsComponent,
        ExerciseDetailsComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        BsDropdownModule.forRoot(),
        BsDatepickerModule.forRoot(),
        PaginationModule.forRoot(),
        ButtonsModule.forRoot(),
        TabsModule.forRoot(),
        RouterModule.forRoot(appRoutes),
        JwtModule.forRoot({
            config: {
                tokenGetter: tokenGetter,
                whitelistedDomains: ['localhost:5002', 'rwherber.com:91'],
                blacklistedRoutes: ['localhost:5002/auth']
            }
        })
    ],
    providers: [
        AuthService,
        ErrorInterceptorProvider,
        AlertifyService,
        AuthGuard,
        UserService,
        ExerciseService,
        WorkoutPlanService,
        ExerciseDetailsResolver,
        PlanOverviewResolver,
        WorkoutDetailsResolver
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
