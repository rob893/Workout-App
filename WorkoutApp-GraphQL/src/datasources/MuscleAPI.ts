import { Muscle, Exercise } from '../models/workout-api/exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';

export class MuscleAPI extends WorkoutAppAPI {
  public getMuscles(): Promise<Muscle[]> {
    return this.get<Muscle[]>('muscles');
  }

  public getMuscleById(id: number): Promise<Muscle | null> {
    return this.get(`muscles/${id}`);
  }

  public getPrimaryExercisesForMuscle(id: number): Promise<Exercise[]> {
    return this.get(`muscles/${id}/primaryExercises`);
  }

  public getSecondaryExercisesForMuscle(id: number): Promise<Exercise[]> {
    return this.get(`muscles/${id}/secondaryExercises`);
  }
}
