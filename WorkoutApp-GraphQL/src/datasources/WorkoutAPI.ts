import { WorkoutAppAPI } from './WorkoutAppAPI';
import {
  WorkoutDetailed,
  ScheduledWorkout,
  ScheduledWorkoutDetailed,
  ExerciseGroup
} from '../models/workout-api/workout';
import { User } from '../models/workout-api/user';
import { CursorPaginatedResponse } from '../models/workout-api/common';

export class WorkoutAPI extends WorkoutAppAPI {
  public getScheduledWorkout(id: number): Promise<ScheduledWorkout | null> {
    return this.get(`scheduledWorkouts/${id}`);
  }

  public getScheduledWorkoutDetailed(id: number): Promise<ScheduledWorkoutDetailed | null> {
    return this.get(`scheduledWorkouts/${id}/detailed`);
  }

  public getScheduledWorkoutAttendees(id: number): Promise<CursorPaginatedResponse<User>> {
    return this.get(`scheduledWorkouts/${id}/attendees`);
  }

  public getScheduledWorkoutAdHocExercises(id: number): Promise<CursorPaginatedResponse<ExerciseGroup>> {
    return this.get(`scheduledWorkouts/${id}/adHocExercises`);
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

  public getWorkoutsDetailed(): Promise<CursorPaginatedResponse<WorkoutDetailed>> {
    return this.get('workouts/detailed');
  }

  public async getWorkoutDetailed(id: number): Promise<WorkoutDetailed | null> {
    return this.get(`workouts/${id}/detailed`);
  }
}
