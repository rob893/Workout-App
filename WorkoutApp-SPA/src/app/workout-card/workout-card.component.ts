import { Component, OnInit, Input } from '@angular/core';
import { WorkoutDay } from '../_models/workoutDay';
import * as moment from 'moment';

@Component({
    selector: 'app-workout-card',
    templateUrl: './workout-card.component.html',
    styleUrls: ['./workout-card.component.css']
})
export class WorkoutCardComponent implements OnInit {
    @Input()
    public workoutDay: WorkoutDay;


    public constructor() { }

    public ngOnInit(): void { }

    public getFormattedTimeForWorkout(workoutIndex: number): string {
        if (!this.workoutDay.workouts || !this.workoutDay.workouts[workoutIndex]) {
            return '';
        }

        return moment(this.workoutDay.workouts[workoutIndex].createdOnDate).format('ha');
    }
}
