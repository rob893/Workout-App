export interface ExerciseDetailed {
  id: number;
  name: string;
  primaryMuscle: Muscle | null;
  secondaryMuscle: Muscle | null;
  exerciseSteps: ExerciseStep[];
  equipment: Equipment[];
  exerciseCategorys: ExerciseCategory[];
}

export interface Muscle {
  id: number;
  name: string;
  description: string;
}

export interface ExerciseStep {
  exerciseStepNumber: number;
  description: string;
}

export interface Equipment {
  id: number;
  name: string;
}

export interface ExerciseCategory {
  id: number;
  name: string;
}
