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

    public async createScheduledWorkout(newWorkout: { workoutId: number, scheduledDateTime: string }) {
        const res = await this.post('scheduledWorkouts', new Object({...newWorkout}));

        return res;
    }

    public async startScheduledWorkout(id: number) {
        const res = await this.patch(`scheduledWorkouts/${id}/startWorkout`);

        return res;
    }

    public async getWorkoutsDetailed() {
        const workouts = await this.get('workouts/detailed');

        return workouts;
    }

    public async getWorkoutDetailed(id: number) {
        const workouts = await this.get(`workouts/${id}/detailed`);

        return workouts;
    }
}