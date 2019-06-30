import { Workout } from './workout';
import { User } from './user';

export interface ScheduledWorkout {
    adHocExercises: [];
    completedDateTime: Date;
    extraSchUsrWoAttendees: [];
    id: number;
    scheduledDateTime: Date;
    startedDateTime: Date;
    user?: User;
    userId: number;
    workout: Workout;
    workoutId: number;
}

