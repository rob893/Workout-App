import { WorkoutAppAPI } from './WorkoutAppAPI';
import {
  WorkoutDetailed,
  ScheduledWorkout,
  ScheduledWorkoutDetailed,
  ExerciseGroup
} from '../models/workout-api/workout';
import { User } from '../models/workout-api/user';
import { CursorPaginatedResponse } from '../models/workout-api/common';
import { CursorPagination } from '../models/workout-api/queryParams';

export class WorkoutAPI extends WorkoutAppAPI {
  public getScheduledWorkout(id: number): Promise<ScheduledWorkout | null> {
    return this.get(`scheduledWorkouts/${id}`);
  }

  public getScheduledWorkoutDetailed(id: number): Promise<ScheduledWorkoutDetailed | null> {
    return this.get(`scheduledWorkouts/${id}/detailed`);
  }

  public getScheduledWorkoutAttendees(
    id: number,
    queryParams: CursorPagination
  ): Promise<CursorPaginatedResponse<User>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`scheduledWorkouts/${id}/attendees?${query}`);
  }

  public getScheduledWorkoutAdHocExercises(
    id: number,
    queryParams: CursorPagination
  ): Promise<CursorPaginatedResponse<ExerciseGroup>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`scheduledWorkouts/${id}/adHocExercises?${query}`);
  }

  public createScheduledWorkout(newWorkout: {
    workoutId: number;
    scheduledDateTime: string;
  }): Promise<ScheduledWorkout> {
    return this.post('scheduledWorkouts', { ...newWorkout });
  }

  public startScheduledWorkout(id: number): Promise<any> {
    return this.patch(`scheduledWorkouts/${id}/startWorkout`);
  }

  public getWorkoutsDetailed(queryParams: CursorPagination): Promise<CursorPaginatedResponse<WorkoutDetailed>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`workouts/detailed?${query}`);
  }

  public async getWorkoutDetailed(id: number): Promise<WorkoutDetailed | null> {
    return this.get(`workouts/${id}/detailed`);
  }
}
