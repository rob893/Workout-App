import { Exercise } from '../entities/Exercise';
import { WorkoutAppAPI } from './WorkoutAppAPI';

export class ExerciseAPI extends WorkoutAppAPI {
    public getExercises(): Promise<Exercise[]> {
        return this.get<Exercise[]>('exercises/detailed');
    }

    public async getExerciseById(id: string): Promise<Exercise | null> {
        const exercise = await this.get<Exercise>(`exercises/${id}/detailed`);

        if (!exercise) {
            return null;
        }

        return exercise;
    }
}
