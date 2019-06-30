import { Component, OnInit, Input } from '@angular/core';
import { WorkoutDay } from '../_models/workoutDay';
import * as moment from 'moment';
import { Router } from '@angular/router';

@Component({
    selector: 'app-workout-day',
    templateUrl: './workout-day.component.html',
    styleUrls: ['./workout-day.component.css']
})
export class WorkoutDayComponent implements OnInit {
    @Input()
    public workoutDay: WorkoutDay;
    public bgColor: string = 'white';
    public textColor: string = 'black';

    private router: Router;
    
    public constructor(router: Router) { 
        this.router = router;
    }

    public ngOnInit(): void {
        if (this.workoutDay && this.workoutDay.workouts) {
            let workoutComplete: boolean = true;

            for (let workout of this.workoutDay.workouts) {
                if (!workout.createdOnDate) {
                    workoutComplete = false;
                }
            }

            this.bgColor = workoutComplete ? '#4CAF50' : '#F44336';
            this.textColor = this.workoutDay.workouts ? 'white' : 'black';
        }
    }

    public getFormattedTimeForWorkout(workoutIndex: number): string {
        if (!this.workoutDay.workouts || !this.workoutDay.workouts[workoutIndex]) {
            return '';
        }

        return moment(this.workoutDay.workouts[workoutIndex].createdOnDate).format('ha');
    }

    public clickedOn(): void {
        this.router.navigate(['/workoutDetails'], { queryParams: { date: this.workoutDay.formattedDate }});
    }
}
