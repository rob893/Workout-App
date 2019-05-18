import { Workout } from './workout';

export interface WorkoutDay {
    dayName: string;
    formattedDate: string;
    workouts?: Workout[];
}