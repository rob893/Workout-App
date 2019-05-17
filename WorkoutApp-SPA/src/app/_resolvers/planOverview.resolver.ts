import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { WorkoutPlan } from '../_models/workoutPlan';
import { WorkoutPlanService } from '../_services/workoutPlan.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class PlanOverviewResolver implements Resolve<WorkoutPlan> {
    
    private woPlanService: WorkoutPlanService;
    private alertify: AlertifyService;
    private router: Router;


    public constructor(woPlanService: WorkoutPlanService, alertify: AlertifyService, router: Router) {
        this.woPlanService = woPlanService;
        this.alertify = alertify;
        this.router = router;
    }
    
    public resolve(route: ActivatedRouteSnapshot): Observable<WorkoutPlan> {
        return this.woPlanService.getWorkoutPlansForUser(3).pipe(
            catchError(error => {
                this.alertify.error(error);
                this.router.navigate(['/home']);
                return of(null);
            })
        )
    }
}