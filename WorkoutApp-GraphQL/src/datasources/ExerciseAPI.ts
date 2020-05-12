import { Exercise } from '../models/workout-api/exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';
import { CursorPaginatedResponse } from '../models/workout-api/common';

export class ExerciseAPI extends WorkoutAppAPI {
  public getExercises(): Promise<CursorPaginatedResponse<Exercise>> {
    return this.get('exercises');
  }

  public getExerciseById(id: number): Promise<Exercise | null> {
    return this.get(`exercises/${id}`);
  }
}
