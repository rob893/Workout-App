import { Workout } from './workout';

export interface WorkoutPlan {
    id: number,
    userId: number,
    workouts: Workout[]
}