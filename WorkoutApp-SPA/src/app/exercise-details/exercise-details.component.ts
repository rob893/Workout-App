import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Exercise } from '../_models/exercise';

@Component({
    selector: 'app-exercise-details',
    templateUrl: './exercise-details.component.html',
    styleUrls: ['./exercise-details.component.css']
})
export class ExerciseDetailsComponent implements OnInit {
    public exercise: Exercise;

    private route: ActivatedRoute;


    public constructor(route: ActivatedRoute) { 
        this.route = route;
    }

    public ngOnInit(): void {
        this.route.data.subscribe(data => {
            this.exercise = data['exercise'];
        });

        console.log(this.exercise);
    }
}
