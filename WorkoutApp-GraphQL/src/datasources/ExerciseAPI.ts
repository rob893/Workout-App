import { Exercise } from '../models/workout-api/exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';

export class ExerciseAPI extends WorkoutAppAPI {
  public getExercises(): Promise<Exercise[]> {
    return this.get('exercises');
  }

  public getExerciseById(id: number): Promise<Exercise | null> {
    return this.get(`exercises/${id}`);
  }
}
