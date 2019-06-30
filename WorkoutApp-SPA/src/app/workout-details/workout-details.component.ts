import { Component, OnInit } from '@angular/core';
import { Workout } from '../_models/workout';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { ScheduledWorkout } from '../_models/scheduledWorkout';

@Component({
    selector: 'app-workout-details',
    templateUrl: './workout-details.component.html',
    styleUrls: ['./workout-details.component.css']
})
export class WorkoutDetailsComponent implements OnInit {
    public workouts: ScheduledWorkout[] = [];
    public formattedDate: string = '';
    public timeMap: Map<ScheduledWorkout, string> = new Map<ScheduledWorkout, string>();

    private route: ActivatedRoute;


    public constructor(route: ActivatedRoute) { 
        this.route = route;
    }

    public ngOnInit(): void {
        this.route.data.subscribe(data => {
            this.workouts = data['workouts'].results;
        });
        
        if (this.workouts.length > 0) {
            this.formattedDate = moment(this.workouts[0].scheduledDateTime).format('MMMM Do YYYY');
        }

        for (let workout of this.workouts) {
            this.timeMap.set(workout, moment(workout.scheduledDateTime).format('ha'));
        }
    }
}
