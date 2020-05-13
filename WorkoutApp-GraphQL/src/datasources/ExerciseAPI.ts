import { Exercise, Equipment, ExerciseCategory } from '../models/workout-api/exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';
import { CursorPaginatedResponse } from '../models/workout-api/common';

export class ExerciseAPI extends WorkoutAppAPI {
  public getExercises(): Promise<CursorPaginatedResponse<Exercise>> {
    return this.get('exercises');
  }

  public getExerciseById(id: number): Promise<Exercise | null> {
    return this.get(`exercises/${id}`);
  }

  public getEquipmentForExercise(id: number): Promise<CursorPaginatedResponse<Equipment>> {
    return this.get(`exercises/${id}/equipment`);
  }

  public getExerciseCategoriesForExercise(id: number): Promise<CursorPaginatedResponse<ExerciseCategory>> {
    return this.get(`exercises/${id}/exerciseCategories`);
  }
}
