import { Equipment, Exercise } from '../models/workout-api/exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';
import { CursorPaginatedResponse } from '../models/workout-api/common';
import { CursorPagination } from '../models/workout-api/queryParams';

export class EquipmentAPI extends WorkoutAppAPI {
  public getAllEquipment(queryParams: CursorPagination): Promise<CursorPaginatedResponse<Equipment>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`equipment?${query}`);
  }

  public getEquipmentById(id: number): Promise<Equipment | null> {
    return this.get(`equipment/${id}`);
  }

  public getExercisesForEquipment(
    id: number,
    queryParams: CursorPagination
  ): Promise<CursorPaginatedResponse<Exercise>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`equipment/${id}/exercises?${query}`);
  }
}
