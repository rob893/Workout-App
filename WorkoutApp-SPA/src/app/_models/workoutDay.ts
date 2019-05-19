import { Workout } from './workout';

export interface WorkoutDay {
    dayName: string;
    formattedDate: string;
    dayOfMonth: string;
    workouts?: Workout[];
}