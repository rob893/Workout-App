import {
  User,
  UserToRegister,
  UserLogin,
  UserLoginResponse,
  RegisterUserResponse,
  RefreshTokenResponse
} from '../models/workout-api/User';
import { WorkoutAppAPI } from './WorkoutAppAPI';
import { WorkoutInvitation, ScheduledWorkoutDetailed, ScheduledWorkout } from '../models/workout-api/Workout';

export class UserAPI extends WorkoutAppAPI {
  public getAllUsers(): Promise<User[]> {
    return this.get<User[]>('users');
  }

  public async getUserById(id: number): Promise<User | null> {
    const user = await this.get<User>(`users/${id}`);

    if (!user) {
      return null;
    }

    return user;
  }

  public getSentWorkoutInvitationsForUser(id: number, status?: string): Promise<WorkoutInvitation[]> {
    if (status) {
      return this.get<WorkoutInvitation[]>(`users/${id}/workoutInvitations/sent?status=${status}`);
    }
    return this.get<WorkoutInvitation[]>(`users/${id}/workoutInvitations/sent`);
  }

  public getReceivedWorkoutInvitationsForUser(id: number, status?: string): Promise<WorkoutInvitation[]> {
    if (status) {
      return this.get<WorkoutInvitation[]>(`users/${id}/workoutInvitations?status=${status}`);
    }
    return this.get<WorkoutInvitation[]>(`users/${id}/workoutInvitations`);
  }

  public registerUser(userToCreate: UserToRegister): Promise<RegisterUserResponse> {
    return this.post<User>('auth/register', { ...userToCreate });
  }

  public login(userLogin: UserLogin): Promise<UserLoginResponse> {
    return this.post<UserLoginResponse>('auth/login', { ...userLogin });
  }

  public getScheduledWorkoutsForUser(userId: number): Promise<ScheduledWorkout[]> {
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
