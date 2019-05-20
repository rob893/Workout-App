import { Injectable } from "@angular/core";
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Exercise } from '../_models/exercise';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ExerciseService } from '../_services/exercise.service';

@Injectable()
export class ExerciseDetailsResolver implements Resolve<Exercise> {

    private alertify: AlertifyService;
    private exerciseService: ExerciseService;
    private router: Router;


    public constructor(alertify: AlertifyService, exerciseService: ExerciseService, router: Router) {
        this.alertify = alertify;
        this.exerciseService = exerciseService;
        this.router = router;
    }

    public resolve(route: ActivatedRouteSnapshot): Observable<Exercise> {
        let exerciseId = route.params['id'];

        return this.exerciseService.getExerciseDetailed(exerciseId).pipe(
            catchError(error => {
                this.alertify.error(error);
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}