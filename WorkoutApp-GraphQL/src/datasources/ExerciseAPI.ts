import { ExerciseDetailed } from '../models/workout-api/Exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';

export class ExerciseAPI extends WorkoutAppAPI {
  public getExercises(): Promise<ExerciseDetailed[]> {
    return this.get<ExerciseDetailed[]>('exercises/detailed');
  }

  public async getExerciseById(id: number): Promise<ExerciseDetailed | null> {
    const exercise = await this.get<ExerciseDetailed>(`exercises/${id}/detailed`);

    if (!exercise) {
      return null;
    }

    return exercise;
  }
}
