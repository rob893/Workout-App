import { Component, OnInit, Input } from '@angular/core';
import { WorkoutDay } from '../_models/workoutDay';
import * as moment from 'moment';

@Component({
    selector: 'app-workout-day',
    templateUrl: './workout-day.component.html',
    styleUrls: ['./workout-day.component.css']
})
export class WorkoutDayComponent implements OnInit {
    @Input()
    public workoutDay: WorkoutDay;

    public dayOfMonth: string = '';
    
    public constructor() { }

    public ngOnInit(): void {
        // if (this.workoutDay !== null) {
        //     this.dayOfMonth = moment(this.workoutDay.formattedDate).format('d');
        // }
    }

    public getFormattedTimeForWorkout(workoutIndex: number): string {
        if (!this.workoutDay.workouts || !this.workoutDay.workouts[workoutIndex]) {
            return '';
        }

        return moment(this.workoutDay.workouts[workoutIndex].date).format('ha');
    }
}
