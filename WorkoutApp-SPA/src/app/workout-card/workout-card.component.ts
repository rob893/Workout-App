import { Component, OnInit, Input } from '@angular/core';
import { Workout } from '../_models/workout';
import * as moment from 'moment';

@Component({
    selector: 'app-workout-card',
    templateUrl: './workout-card.component.html',
    styleUrls: ['./workout-card.component.css']
})
export class WorkoutCardComponent implements OnInit {
    @Input()
    public workout: Workout;

    public formattedDate: string;


    constructor() { }

    ngOnInit() {
        this.formattedDate = moment(this.workout.date).format('MMMM Do YYYY');
    }

}
