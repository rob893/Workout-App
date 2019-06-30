import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

    public model: any = {};
    public authService: AuthService;
    public photoUrl: string;

    private alertify: AlertifyService;
    private router: Router;


    public constructor(authService: AuthService, alertify: AlertifyService, router: Router) {
        this.authService = authService;
        this.alertify = alertify;
        this.router = router;
    }

    public ngOnInit(): void {
    }


    public login(): void {
        this.authService.login(this.model).subscribe(next => {
            this.alertify.success('Logged in successfully!');
        }, error => {
            this.alertify.error(error);
        }, () => {
            this.router.navigate(['/scheduledWorkouts']);
        });
    }

    public loggedIn(): boolean {
        return this.authService.loggedIn();
    }

    public logout(): void {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        this.authService.decodeToken = null;
        this.authService.currentUser = null;
        this.alertify.message("Logged Out");
        this.router.navigate(['/home']);
    }
}
