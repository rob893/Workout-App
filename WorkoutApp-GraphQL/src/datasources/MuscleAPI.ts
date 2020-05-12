import { Muscle, Exercise } from '../models/workout-api/exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';
import { CursorPaginatedResponse } from '../models/workout-api/common';

export class MuscleAPI extends WorkoutAppAPI {
  public getMuscles(): Promise<CursorPaginatedResponse<Muscle>> {
    return this.get('muscles');
  }

  public getMuscleById(id: number): Promise<Muscle | null> {
    return this.get(`muscles/${id}`);
  }

  public getPrimaryExercisesForMuscle(id: number): Promise<CursorPaginatedResponse<Exercise>> {
    return this.get(`muscles/${id}/primaryExercises`);
  }

  public getSecondaryExercisesForMuscle(id: number): Promise<CursorPaginatedResponse<Exercise>> {
    return this.get(`muscles/${id}/secondaryExercises`);
  }
}
