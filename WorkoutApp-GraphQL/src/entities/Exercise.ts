export interface Exercise {
    id: string;
    name: string;
    primaryMuscle: Muscle;
    secondaryMuscle: Muscle;
    exerciseSteps: ExerciseStep[];
    equipment: Equipment[];
    exerciseCategorys: ExerciseCategory[];
}

export interface Muscle {
    id: string;
    name: string;
}

export interface ExerciseStep {
    exerciseStepNumber: number;
    description: string;
}

export interface Equipment {
    id: string;
    name: string;
}

export interface ExerciseCategory {
    id: string;
    name: string;
}