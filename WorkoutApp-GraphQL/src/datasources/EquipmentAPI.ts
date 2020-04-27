import { Equipment, Exercise } from '../models/workout-api/Exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';

export class EquipmentAPI extends WorkoutAppAPI {
  public getAllEquipment(): Promise<Equipment[]> {
    return this.get<Equipment[]>('equipment');
  }

  public getEquipmentById(id: number): Promise<Equipment | null> {
    return this.get<Equipment>(`equipment/${id}`);
  }

  public getExercisesForEquipment(id: number): Promise<Exercise[]> {
    return this.get(`equipment/${id}/exercises`);
  }
}
