import { ExerciseGroup } from './exerciseGroup';

export interface Workout {
    id: number,
    workoutPlanId: number,
    date: Date,
    complete: boolean,
    exerciseGroups: ExerciseGroup[]
}