import { WorkoutAppAPI } from './WorkoutAppAPI';
import { ExerciseCategory, Exercise } from '../models/workout-api/Exercise';

export class ExerciseCategoryAPI extends WorkoutAppAPI {
  public getExerciseCategory(id: number): Promise<ExerciseCategory | null> {
    return this.get(`exerciseCategories/${id}`);
  }

  public getExerciseCategories(): Promise<ExerciseCategory[]> {
    return this.get(`exerciseCategories`);
  }

  public getExercisesForExerciseCategory(id: number): Promise<Exercise[]> {
    return this.get(`exerciseCategories/${id}/exercises`);
  }
}
