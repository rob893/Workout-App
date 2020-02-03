import { RESTDataSource, RequestOptions } from 'apollo-datasource-rest';
import { User, UserToRegister, UserLogin, UserLoginResponse } from "../entities/User";
import { IUserAPI } from "../interfaces/IUserAPI";

export class UserAPI extends RESTDataSource implements IUserAPI {

    public constructor() {
        super();
        this.baseURL = process.env.WORKOUT_APP_API_URL || 'http://localhost:5002';
    }

    public willSendRequest(request: RequestOptions): void {
        if (this.context && this.context.token) {
            request.headers.set('authorization', this.context.token);
        }
    }

    public async getAllUsers(): Promise<User[]> {
        const users = await this.get<User[]>('users');

        return users;
    }

    public async getUserById(id: string): Promise<User | null> {
        const user = await this.get<User>(`users/${id}`);

        if (!user) {
            return null;
        }

        return user;
    }

    public async registerUser(userToCreate: UserToRegister): Promise<User> {
        const res = await this.post<User>('auth/register', new Object({...userToCreate}));

        return res;
    }

    public async login(userLogin: UserLogin): Promise<UserLoginResponse> {
        const res = await this.post<UserLoginResponse>('auth/login', new Object({...userLogin}));

        return res;
    }
}