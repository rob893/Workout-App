import {
  User,
  UserToRegister,
  UserLogin,
  UserLoginResponse,
  RegisterUserResponse,
  RefreshTokenResponse
} from '../models/workout-api/User';
import { WorkoutAppAPI } from './WorkoutAppAPI';
import { WorkoutInvitation, ScheduledWorkout } from '../models/workout-api/Workout';
import { WorkoutInvitationQueryParams } from '../models/workout-api/queryParams';
import { SchemaWorkoutInvitationPage } from '../models/schema';

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

  public async getSentWorkoutInvitationsForUser(
    id: number,
    queryParams: WorkoutInvitationQueryParams = {}
  ): Promise<any> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    const requestUrl = `users/${id}/workoutInvitations/sent?${query}`;
    const workoutInvitations = await this.get<WorkoutInvitation[]>(requestUrl);
    const reqres = this.requestMap.get(`${this.baseURL}/${requestUrl}`);
    let pageInfo: any = {};
    if (reqres) {
      pageInfo.pageNumber = reqres.response.headers.get('X-Pagination-PageNumber');
      pageInfo.pageSize = reqres.response.headers.get('X-Pagination-PageSize');
      pageInfo.totalItems = reqres.response.headers.get('X-Pagination-TotalItems');
      pageInfo.totalPages = reqres.response.headers.get('X-Pagination-TotalPages');
    }
    return {
      workoutInvitations,
      pageInfo
    };
  }

  public getReceivedWorkoutInvitationsForUser(
    id: number,
    queryParams: WorkoutInvitationQueryParams = {}
  ): Promise<WorkoutInvitation[]> {
    const query = WorkoutAppAPI.buildQuery(queryParams);
    return this.get<WorkoutInvitation[]>(`users/${id}/workoutInvitations?${query}`);
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
