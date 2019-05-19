import { Component, OnInit } from '@angular/core';
import { Workout } from '../_models/workout';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment';

@Component({
    selector: 'app-workout-details',
    templateUrl: './workout-details.component.html',
    styleUrls: ['./workout-details.component.css']
})
export class WorkoutDetailsComponent implements OnInit {
    public workouts: Workout[] = [];
    public formattedDate: string;

    private route: ActivatedRoute;


    public constructor(route: ActivatedRoute) { 
        this.route = route;
    }

    public ngOnInit(): void {
        this.route.data.subscribe(data => {
            this.workouts = data['workouts'].results;
        });
        
        if (this.workouts.length > 0) {
            this.formattedDate = moment(this.workouts[0].date).format('MMMM Do YYYY');
        }
    }
}
