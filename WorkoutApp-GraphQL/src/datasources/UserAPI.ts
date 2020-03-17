import { User, UserToRegister, UserLogin, UserLoginResponse } from '../entities/User';
import { WorkoutAppAPI } from './WorkoutAppAPI';

export class UserAPI extends WorkoutAppAPI {
    public getAllUsers(): Promise<User[]> {
        return this.get<User[]>('users');
    }

    public async getUserById(id: string): Promise<User | null> {
        const user = await this.get<User>(`users/${id}`);

        if (!user) {
            return null;
        }

        return user;
    }

    public registerUser(userToCreate: UserToRegister): Promise<User> {
        return this.post<User>('auth/register', { ...userToCreate });
    }

    public login(userLogin: UserLogin): Promise<UserLoginResponse> {
        return this.post<UserLoginResponse>('auth/login', { ...userLogin });
    }

    public getScheduledWorkoutsForUser(userId: string): Promise<any[]> {
        return this.get(`users/${userId}/scheduledWorkouts`);
    }

    public refreshToken(refreshTokenInput: {
        token: string;
        refreshToken: string;
        source: string;
    }): Promise<{ token: string; refreshToken: string }> {
        return this.post('auth/refreshToken', { ...refreshTokenInput });
    }
}
