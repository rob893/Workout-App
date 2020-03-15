import { RESTDataSource, RequestOptions, Response, Request } from 'apollo-datasource-rest';
import { Exercise } from '../entities/Exercise';
import { WorkoutAppContext } from '..';

export class ExerciseAPI extends RESTDataSource<WorkoutAppContext> {
    public constructor() {
        super();
        this.baseURL = process.env.WORKOUT_APP_API_URL || 'http://localhost:5002';
    }

    public async getExercises(): Promise<Exercise[]> {
        const exercises = await this.get<Exercise[]>('exercises/detailed');

        return exercises;
    }

    public async getExerciseById(id: string): Promise<Exercise | null> {
        const exercise = await this.get<Exercise>(`exercises/${id}/detailed`);

        if (!exercise) {
            return null;
        }

        return exercise;
    }

    protected willSendRequest(request: RequestOptions): void {
        if (this.context && this.context.token) {
            request.headers.set('authorization', this.context.token);
        }
    }

    protected didReceiveResponse<TResult = any>(response: Response, request: Request): Promise<TResult> {
        if (response.status === 401 && response.headers.has('token-expired')) {
            console.log(response.headers);
            this.context.response.setHeader('token-expired', 'true');
        }

        return super.didReceiveResponse(response, request);
    }
}
