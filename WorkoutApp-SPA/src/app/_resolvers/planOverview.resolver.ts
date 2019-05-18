import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { WorkoutPlan } from '../_models/workoutPlan';
import { WorkoutPlanService } from '../_services/workoutPlan.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class PlanOverviewResolver implements Resolve<WorkoutPlan> {
    
    private woPlanService: WorkoutPlanService;
    private alertify: AlertifyService;
    private authService: AuthService;
    private router: Router;


    public constructor(woPlanService: WorkoutPlanService, alertify: AlertifyService, router: Router, authService: AuthService) {
        this.woPlanService = woPlanService;
        this.alertify = alertify;
        this.router = router;
        this.authService = authService
    }
    
    public resolve(route: ActivatedRouteSnapshot): Observable<WorkoutPlan> {
        let currentUserId = this.authService.currentUser.id;

        return this.woPlanService.getWorkoutPlansForUser(currentUserId).pipe(
            catchError(error => {
                this.alertify.error(error);
                this.router.navigate(['/home']);
                return of(null);
            })
        )
    }
}