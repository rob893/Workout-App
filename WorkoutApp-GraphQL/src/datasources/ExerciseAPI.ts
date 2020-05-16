import { Exercise, Equipment, ExerciseCategory } from '../models/workout-api/exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';
import { CursorPaginatedResponse } from '../models/workout-api/common';
import { CursorPagination } from '../models/workout-api/queryParams';

export class ExerciseAPI extends WorkoutAppAPI {
  public getExercises(queryParams: CursorPagination): Promise<CursorPaginatedResponse<Exercise>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`exercises?${query}`);
  }

  public getExerciseById(id: number): Promise<Exercise | null> {
    return this.get(`exercises/${id}`);
  }

  public getEquipmentForExercise(
    id: number,
    queryParams: CursorPagination
  ): Promise<CursorPaginatedResponse<Equipment>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`exercises/${id}/equipment?${query}`);
  }

  public getExerciseCategoriesForExercise(
    id: number,
    queryParams: CursorPagination
  ): Promise<CursorPaginatedResponse<ExerciseCategory>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`exercises/${id}/exerciseCategories?${query}`);
  }
}
