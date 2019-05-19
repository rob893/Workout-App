import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Workout } from '../_models/workout';
import { Observable, of } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';
import { catchError } from 'rxjs/operators';
import { PaginatedResults } from '../_models/pagination';
import { WorkoutParams } from '../_models/queryParams';

@Injectable()
export class WorkoutDetailsResolver implements Resolve<PaginatedResults<Workout>> {

    private authService: AuthService;
    private alertify: AlertifyService;
    private userService: UserService;
    private router: Router;


    public constructor(authService: AuthService, alertify: AlertifyService, userService: UserService, router: Router) {
        this.authService = authService;
        this.alertify = alertify;
        this.userService = userService;
        this.router = router;
    }

    public resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResults<Workout>> {
        let currentUserId = this.authService.currentUser.id;
        
        let woParams: WorkoutParams = {
            minDate: route.queryParams['date'],
            maxDate: route.queryParams['date']
        };

        return this.userService.getWorkoutsForUser(currentUserId, woParams).pipe(
            catchError( error => {
                this.alertify.error(error);
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}