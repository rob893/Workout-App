import { Exercise } from './exercise';

export interface ExerciseGroup {
    id: number,
    exerciseId: number,
    workoutId: number,
    sets: number,
    repetitions: number,
    exercise: Exercise
}