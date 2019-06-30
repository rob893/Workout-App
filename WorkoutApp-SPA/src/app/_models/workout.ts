import { ExerciseGroup } from './exerciseGroup';
import { User } from './user';

export interface Workout {
    color: string;
    createdByUser: User;
    createdByUserId: number;
    createdOnDate: Date;
    exerciseGroups: ExerciseGroup[];
    id: number;
    isDeleted: boolean;
    label: string;
    lastModifiedDate: Date;
    shareable: boolean;
    workoutCopiedFrom?: Workout;
}