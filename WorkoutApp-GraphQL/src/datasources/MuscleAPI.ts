import { Muscle, Exercise } from '../models/workout-api/exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';
import { CursorPaginatedResponse } from '../models/workout-api/common';
import { CursorPagination } from '../models/workout-api/queryParams';

export class MuscleAPI extends WorkoutAppAPI {
  public getMuscles(queryParams: CursorPagination): Promise<CursorPaginatedResponse<Muscle>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`muscles?${query}`);
  }

  public getMuscleById(id: number): Promise<Muscle | null> {
    return this.get(`muscles/${id}`);
  }

  public getPrimaryExercisesForMuscle(
    id: number,
    queryParams: CursorPagination
  ): Promise<CursorPaginatedResponse<Exercise>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`muscles/${id}/primaryExercises?${query}`);
  }

  public getSecondaryExercisesForMuscle(
    id: number,
    queryParams: CursorPagination
  ): Promise<CursorPaginatedResponse<Exercise>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`muscles/${id}/secondaryExercises?${query}`);
  }
}
