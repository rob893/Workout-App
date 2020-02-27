import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Apollo } from 'apollo-angular';
import { GraphQLQueries } from '../core/queries';

interface LoginResponse {
    login: {
        token?: string;
        user?: {
            username?: string;
            firstName?: string;
            lastName?: string;
            email?: string;
        }
    }
}

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    private readonly apollo: Apollo;


    public constructor(apollo: Apollo) {
        this.apollo = apollo;
    }

    public login(username: string, password: string) {
        return this.apollo.mutate<LoginResponse, { userCredentials: { username: string; password: string; } }>({
            mutation: GraphQLQueries.login,
            variables: {
                userCredentials: {
                    username,
                    password
                }
            }
        }).pipe(
            map(({ data: { login } }) =>  login)
        );
    }
}
