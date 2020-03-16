import { WorkoutAppAPIError } from '../entities/WorkoutAppAPIError';

export class TypeGuards {
    public static isWorkoutAppAPIError(error: any): error is WorkoutAppAPIError {
        return (
            typeof (error as WorkoutAppAPIError).detail === 'string' &&
            Array.isArray((error as WorkoutAppAPIError).errors) &&
            typeof (error as WorkoutAppAPIError).instance === 'string' &&
            typeof (error as WorkoutAppAPIError).status === 'number' &&
            typeof (error as WorkoutAppAPIError).title === 'string' &&
            typeof (error as WorkoutAppAPIError).type === 'string'
        );
    }
}
