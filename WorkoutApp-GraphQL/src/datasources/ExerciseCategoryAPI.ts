import { WorkoutAppAPI } from './WorkoutAppAPI';
import { ExerciseCategory, Exercise } from '../models/workout-api/exercise';
import { CursorPaginatedResponse } from '../models/workout-api/common';
import { CursorPagination } from '../models/workout-api/queryParams';

export class ExerciseCategoryAPI extends WorkoutAppAPI {
  public getExerciseCategory(id: number): Promise<ExerciseCategory | null> {
    return this.get(`exerciseCategories/${id}`);
  }

  public getExerciseCategories(queryParams: CursorPagination): Promise<CursorPaginatedResponse<ExerciseCategory>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`exerciseCategories?${query}`);
  }

  public getExercisesForExerciseCategory(
    id: number,
    queryParams: CursorPagination
  ): Promise<CursorPaginatedResponse<Exercise>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`exerciseCategories/${id}/exercises?${query}`);
  }
}
