import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import * as moment from 'moment';
import { Moment } from 'moment';
import { WorkoutDay } from '../_models/workoutDay';
import { Workout } from '../_models/workout';
import { ScheduledWorkout } from '../_models/scheduledWorkout';

@Component({
    selector: 'app-plan-overview',
    templateUrl: './plan-overview.component.html',
    styleUrls: ['./plan-overview.component.css']
})
export class PlanOverviewComponent implements OnInit {
    public scheduledWorkouts: ScheduledWorkout[] = [];
    public workoutDays: WorkoutDay[] = [];
    public monthName: string = '';

    private route: ActivatedRoute;
    private workoutMap: Map<string, Workout[]> = new Map<string, Workout[]>(); //formatted date string => array of workouts for that day
    

    public constructor(route: ActivatedRoute) { 
        this.route = route;
    }

    public ngOnInit(): void {
        this.route.data.subscribe(data => {
            this.scheduledWorkouts = data['scheduledWorkouts'];
        });
        
        for (let scheduledWorkout of this.scheduledWorkouts) {
            
            let workout = scheduledWorkout.workout;
            let dateKey: string = moment(scheduledWorkout.scheduledDateTime).format('MMMM Do YYYY');

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

        this.monthName = dateIndex.format('MMMM');

        while (dateIndex.isSameOrBefore(toDate, 'day')) {
            let dateKey: string = dateIndex.format('MMMM Do YYYY');

            let workoutDay: WorkoutDay = {
                dayName: dateIndex.format('dddd'),
                formattedDate: dateIndex.format(),
                dayOfMonth: dateIndex.format('D')
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
