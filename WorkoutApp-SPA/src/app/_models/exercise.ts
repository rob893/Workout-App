import { Muscle } from './muscle';
import { Equipment } from './equipment';

export interface Exercise {
    id: number,
    name: string,
    primaryMuscle?: Muscle,
    secondaryMuscle?: Muscle,
    exerciseSteps?: ExerciseStep[],
    equipment?: Equipment[],
    exerciseCategorys?: ExerciseCategory[]
}

export interface ExerciseStep {
    exerciseStepNumber: number,
    description: string
}

export interface ExerciseCategory {
    id: number,
    name: string
}