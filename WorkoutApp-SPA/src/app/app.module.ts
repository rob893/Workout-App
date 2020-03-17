import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';
import { GraphQLModule } from './graphql.module';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { MaterialModule } from './material.module';
import { AuthService } from './auth/auth.service';
import { SidenavListComponent } from './navigation/sidenav-list/sidenav-list.component';
import { HeaderComponent } from './navigation/header/header.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { LoginComponent } from './auth/login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SignupComponent } from './auth/signup/signup.component';

export function tokenGetter(): string {
    return localStorage.getItem('access-token');
}

@NgModule({
    declarations: [
        AppComponent,
        HeaderComponent,
        SidenavListComponent,
        WelcomeComponent,
        LoginComponent,
        SignupComponent
    ],
    imports: [
        MaterialModule,
        FlexLayoutModule,
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        ServiceWorkerModule.register('ngsw-worker.js', {
            enabled: environment.production
        }),
        GraphQLModule,
        HttpClientModule,
        JwtModule.forRoot({
            config: {
                tokenGetter,
                whitelistedDomains: ['localhost:5003', 'localhost:4000', 'rwherber.com'],
                blacklistedRoutes: []
            }
        })
    ],
    providers: [AuthService],
    bootstrap: [AppComponent]
})
export class AppModule {}
