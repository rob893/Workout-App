import { User } from './User';
import { Exercise } from './Exercise';

export interface Workout {
  label: string;
  description: string | null;
  exerciseGroupsUrl: string;
  createdByUserId: number;
  createdByUserUrl: string;
  createdOnDate: string;
  lastModifiedDate: string;
  shareable: boolean;
  isDeleted: boolean;
  id: number;
  url: string;
  detailedUrl: string;
}

export interface WorkoutDetailed {
  createdByUser: User;
  workoutCopiedFrom: Workout | null;
  exerciseGroups: ExerciseGroup[];
  label: string;
  description: string | null;
  exerciseGroupsUrl: string;
  createdByUserId: number;
  createdByUserUrl: string;
  createdOnDate: string;
  lastModifiedDate: string;
  shareable: boolean;
  isDeleted: boolean;
  id: number;
  url: string;
  detailedUrl: string;
}

export interface ExerciseGroup {
  id: number;
  exerciseId: number;
  exercise: Exercise;
  sets: number;
  repetitions: number;
}

export interface WorkoutInvitation {
  inviterId: number;
  inviterUrl: string;
  inviteeId: number;
  inviteeUrl: string;
  scheduledWorkoutId: number;
  scheduledWorkoutUrl: string;
  accepted: boolean;
  declined: boolean;
  status: string;
  respondedAtDateTime: string | null;
  id: number;
  url: string;
  detailedUrl: string;
}
