import { Component, OnInit, Input } from '@angular/core';
import { WorkoutDay } from '../_models/workoutDay';

@Component({
    selector: 'app-workout-calendar',
    templateUrl: './workout-calendar.component.html',
    styleUrls: ['./workout-calendar.component.css']
})
export class WorkoutCalendarComponent implements OnInit {
    @Input()
    public workoutDaysForMonth: WorkoutDay[] = [];
    @Input()
    public monthName: string;
    public daysOfWeek: string[] = ['S', 'M', 'T', 'W', 'T', 'F', 'S'];
    public dayMatrix: WorkoutDay[][] = [[],[],[],[],[]];


    public constructor() { }

    public ngOnInit(): void {
        this.buildDayMatrix();
    }

    private buildDayMatrix(): void {
        let tableIndex = 0;
        let dayIndex = 0;
        
        for (let i = 0; i < 5; i++) {
            for (let j = 0; j < 7; j++) {
                let dayOfWeek = tableIndex % 7;

                if (dayIndex < this.workoutDaysForMonth.length) {
                    let woDay = this.getDayOfWeek(this.workoutDaysForMonth[dayIndex].dayName);

                    if (dayOfWeek === woDay) {
                        this.dayMatrix[i][j] = this.workoutDaysForMonth[dayIndex];
                        dayIndex++;
                    }
                    else {
                        this.dayMatrix[i][j] = null;
                    }
                }
                else {
                    this.dayMatrix[i][j] = null;
                }
                tableIndex++;
            }
        }
    }

    private getDayOfWeek(day: string): number {
        day = day.toLowerCase();
        
        switch(day) {
            case 'sunday':
                return 0;
            case 'monday':
                return 1;
            case 'tuesday':
                return 2;
            case 'wednesday':
                return 3;
            case 'thursday':
                return 4;
            case 'friday':
                return 5;
            case 'saturday':
                return 6;
            default:
                throw new Error("Invalid day");
        }
    }
}
