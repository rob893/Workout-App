import { WorkoutAppAPI } from './WorkoutAppAPI';
import { WorkoutDetailed } from '../models/workout-api/Workout';

export class WorkoutAPI extends WorkoutAppAPI {
  public createScheduledWorkout(newWorkout: { workoutId: number; scheduledDateTime: string }): Promise<any> {
    return this.post('scheduledWorkouts', { ...newWorkout });
  }

  public startScheduledWorkout(id: number): Promise<any> {
    return this.patch(`scheduledWorkouts/${id}/startWorkout`);
  }

  public getWorkoutsDetailed(): Promise<WorkoutDetailed[]> {
    return this.get('workouts/detailed');
  }

  public async getWorkoutDetailed(id: number): Promise<WorkoutDetailed> {
    return this.get(`workouts/${id}/detailed`);
  }
}
