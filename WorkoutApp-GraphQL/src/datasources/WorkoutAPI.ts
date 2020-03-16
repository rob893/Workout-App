import { WorkoutAppAPI } from './WorkoutAppAPI';

export class WorkoutAPI extends WorkoutAppAPI {
    public createScheduledWorkout(newWorkout: { workoutId: number; scheduledDateTime: string }): Promise<any> {
        return this.post('scheduledWorkouts', { ...newWorkout });
    }

    public startScheduledWorkout(id: number): Promise<any> {
        return this.patch(`scheduledWorkouts/${id}/startWorkout`);
    }

    public getWorkoutsDetailed(): Promise<any> {
        return this.get('workouts/detailed');
    }

    public async getWorkoutDetailed(id: number): Promise<any> {
        return this.get(`workouts/${id}/detailed`);
    }
}
