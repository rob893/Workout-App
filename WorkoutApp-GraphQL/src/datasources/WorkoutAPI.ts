import { RESTDataSource, RequestOptions } from 'apollo-datasource-rest';

export class WorkoutAPI extends RESTDataSource {

    public constructor() {
        super();
        this.baseURL = process.env.WORKOUT_APP_API_URL || 'http://localhost:5002';
    }

    public willSendRequest(request: RequestOptions): void {
        if (this.context && this.context.token) {
            request.headers.set('authorization', this.context.token);
        }
    }

    public createScheduledWorkout(newWorkout: { workoutId: number, scheduledDateTime: string }) {
        return this.post('scheduledWorkouts', new Object({...newWorkout}));
    }

    public startScheduledWorkout(id: number) {
        return this.patch(`scheduledWorkouts/${id}/startWorkout`);
    }
}