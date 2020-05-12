import { Equipment, Exercise } from '../models/workout-api/exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';
import { CursorPaginatedResponse } from '../models/workout-api/common';

export class EquipmentAPI extends WorkoutAppAPI {
  public getAllEquipment(): Promise<CursorPaginatedResponse<Equipment>> {
    return this.get('equipment');
  }

  public getEquipmentById(id: number): Promise<Equipment | null> {
    return this.get<Equipment>(`equipment/${id}`);
  }

  public getExercisesForEquipment(id: number): Promise<CursorPaginatedResponse<Exercise>> {
    return this.get(`equipment/${id}/exercises`);
  }
}
