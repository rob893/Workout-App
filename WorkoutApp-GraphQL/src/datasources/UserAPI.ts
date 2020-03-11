import { RESTDataSource, RequestOptions, Response, Request } from 'apollo-datasource-rest';
import { User, UserToRegister, UserLogin, UserLoginResponse } from "../entities/User";
import { ApolloError } from 'apollo-server';

export class UserAPI extends RESTDataSource {

    public constructor() {
        super();
        this.baseURL = process.env.WORKOUT_APP_API_URL || 'http://localhost:5002';
    }

    public willSendRequest(request: RequestOptions): void {
        if (this.context && this.context.token) {
            request.headers.set('authorization', this.context.token);
        }
    }

    protected didReceiveResponse<TResult = any>(response: Response, request: Request): Promise<TResult> {
        if (response.status === 401 && response.headers.has('token-expired')) {
            console.log(response.headers);
            this.context.response.setHeader('token-expired', 'true');
        }

        return super.didReceiveResponse(response, request);
    }

    protected async errorFromResponse(response: Response) {
        const error = await super.errorFromResponse(response);

        if (response.headers.has('token-expired') && response.status === 401) {
            error.extensions.tokenExpired = true;
        }

        return error;
    }

    protected didEncounterError(error: Error, request: Request): void {
        if (this.isApolloError(error) && error.extensions.response) {
            //error.extensions.token
        }
        
        super.didEncounterError(error, request);
    }

    private isApolloError(error: Error): error is ApolloError {
        return (error as ApolloError).extensions !== undefined;
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

    public getScheduledWorkoutsForUser(userId: string): Promise<any[]> {
        return this.get(`users/${userId}/scheduledWorkouts`)
    }
}