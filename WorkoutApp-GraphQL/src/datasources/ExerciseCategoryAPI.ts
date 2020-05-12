import { WorkoutAppAPI } from './WorkoutAppAPI';
import { ExerciseCategory, Exercise } from '../models/workout-api/exercise';
import { CursorPaginatedResponse } from '../models/workout-api/common';

export class ExerciseCategoryAPI extends WorkoutAppAPI {
  public getExerciseCategory(id: number): Promise<ExerciseCategory | null> {
    return this.get(`exerciseCategories/${id}`);
  }

  public getExerciseCategories(): Promise<CursorPaginatedResponse<ExerciseCategory>> {
    return this.get(`exerciseCategories`);
  }

  public getExercisesForExerciseCategory(id: number): Promise<CursorPaginatedResponse<Exercise>> {
    return this.get(`exerciseCategories/${id}/exercises`);
  }
}
