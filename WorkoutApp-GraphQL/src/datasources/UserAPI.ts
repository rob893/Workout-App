import {
  User,
  UserToRegister,
  UserLogin,
  UserLoginResponse,
  RegisterUserResponse,
  RefreshTokenResponse
} from '../models/workout-api/user';
import { WorkoutAppAPI } from './WorkoutAppAPI';
import { WorkoutInvitation, ScheduledWorkout } from '../models/workout-api/workout';
import { WorkoutInvitationQueryParams } from '../models/workout-api/queryParams';
import { CursorPaginatedResponse } from '../models/workout-api/common';

export class UserAPI extends WorkoutAppAPI {
  public getAllUsers(): Promise<CursorPaginatedResponse<User>> {
    return this.get('users');
  }

  public async getUserById(id: number): Promise<User | null> {
    const user = await this.get(`users/${id}`);

    if (!user) {
      return null;
    }

    return user;
  }

  public getSentWorkoutInvitationsForUser(
    id: number,
    queryParams: WorkoutInvitationQueryParams = {}
  ): Promise<CursorPaginatedResponse<WorkoutInvitation>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`users/${id}/workoutInvitations/sent?${query}`);
  }

  public getReceivedWorkoutInvitationsForUser(
    id: number,
    queryParams: WorkoutInvitationQueryParams = {}
  ): Promise<CursorPaginatedResponse<WorkoutInvitation>> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get(`users/${id}/workoutInvitations?${query}`);
  }

  public registerUser(userToCreate: UserToRegister): Promise<RegisterUserResponse> {
    return this.post('auth/register', { ...userToCreate });
  }

  public login(userLogin: UserLogin): Promise<UserLoginResponse> {
    return this.post('auth/login', { ...userLogin });
  }

  public getScheduledWorkoutsForUser(userId: number): Promise<CursorPaginatedResponse<ScheduledWorkout>> {
    return this.get(`users/${userId}/scheduledWorkouts`);
  }

  public refreshToken(refreshTokenInput: {
    token: string;
    refreshToken: string;
    source: string;
  }): Promise<RefreshTokenResponse> {
    return this.post('auth/refreshToken', { ...refreshTokenInput });
  }
}
