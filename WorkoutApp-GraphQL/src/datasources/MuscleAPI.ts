import { Muscle } from '../models/workout-api/Exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';

export class MuscleAPI extends WorkoutAppAPI {
  public getMuscles(): Promise<Muscle[]> {
    return this.get<Muscle[]>('muscles');
  }

  public async getMuscleById(id: number): Promise<Muscle | null> {
    const muscle = await this.get<Muscle>(`muscles/${id}`);

    if (!muscle) {
      return null;
    }

    return muscle;
  }
}
