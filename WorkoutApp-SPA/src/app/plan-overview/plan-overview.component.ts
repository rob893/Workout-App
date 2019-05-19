import { Component, OnInit } from '@angular/core';
import { WorkoutPlan } from '../_models/workoutPlan';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { Moment } from 'moment';
import { WorkoutDay } from '../_models/workoutDay';
import { Workout } from '../_models/workout';

@Component({
    selector: 'app-plan-overview',
    templateUrl: './plan-overview.component.html',
    styleUrls: ['./plan-overview.component.css']
})
export class PlanOverviewComponent implements OnInit {
    public workoutPlan: WorkoutPlan;
    public workoutDays: WorkoutDay[] = [];

    private route: ActivatedRoute;
    private workoutMap: Map<string, Workout[]> = new Map<string, Workout[]>(); //formatted date string => array of workouts for that day
    

    public constructor(route: ActivatedRoute) { 
        this.route = route;
    }

    public ngOnInit(): void {
        this.route.data.subscribe(data => {
            this.workoutPlan = data['workoutPlan'][0];
        });

        for (let workout of this.workoutPlan.workouts) {
            let dateKey: string = moment(workout.date).format('MMMM Do YYYY');

            if (this.workoutMap.has(dateKey)) {
                this.workoutMap.get(dateKey).push(workout);
            }
            else {
                this.workoutMap.set(dateKey, [workout]);
            }
        }
        
        const fromDate: Moment = moment().startOf('month');
        const toDate: Moment = moment().endOf('month');
        const dateIndex: Moment = moment(fromDate);

        while (dateIndex.isSameOrBefore(toDate, 'day')) {
            let dateKey: string = dateIndex.format('MMMM Do YYYY');

            let workoutDay: WorkoutDay = {
                dayName: dateIndex.format('dddd'),
                formattedDate: dateIndex.format('MMMM Do YYYY')
            };

            if (this.workoutMap.has(dateKey)) {
                let workoutsForDay: Workout[] = this.workoutMap.get(dateKey);
                workoutDay.workouts = [];

                for (let workout of workoutsForDay) {
                    workoutDay.workouts.push(workout);
                }
            }

            this.workoutDays.push(workoutDay);
            dateIndex.add(1, 'd');
        }
    }
}
