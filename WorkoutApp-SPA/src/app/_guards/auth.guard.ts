import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CanActivate } from '@angular/router/src/utils/preactivation';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    public path: ActivatedRouteSnapshot[];
    public route: ActivatedRouteSnapshot;

    private authServide: AuthService;
    private router: Router;
    private alertify: AlertifyService;


    public constructor(authService: AuthService, router: Router, alertify: AlertifyService) {
        this.authServide = authService;
        this.router = router;
        this.alertify = alertify;
    }

    public canActivate(): Observable<boolean> | Promise<boolean> | boolean {
        
        if (this.authServide.loggedIn()) {
            return true;
        }

        this.alertify.error("You shall not pass!!!");
        this.router.navigate(['/home']);

        return false;
    }
}
