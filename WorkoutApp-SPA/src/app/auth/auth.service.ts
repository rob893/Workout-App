import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Apollo } from 'apollo-angular';
import { login } from './auth.queries';
import { JwtHelperService } from '@auth0/angular-jwt';

interface LoginResponse {
    login: {
        token: string;
        user: User;
    }
}

interface User {
    id: string;
    userName: string;
    firstName: string;
    lastName: string;
    email: string;
    created: string;
}

interface TokenClaims {
    nameid: string;
    unique_name: string;
    role: string | string[];
    nbf: number;
    exp: number;
    iat: number;
    iss: string;
    aud: string;
}

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    private readonly apollo: Apollo;
    private readonly jwtHelper: JwtHelperService;


    public constructor(apollo: Apollo, jwtHelper: JwtHelperService) {
        this.apollo = apollo;
        this.jwtHelper = jwtHelper;
    }

    public get user(): User | undefined {
        return JSON.parse(localStorage.getItem('user'));
    }

    public get token(): string | undefined {
        return localStorage.getItem('access_token');
    }

    public get decodedToken(): TokenClaims {
        const token = this.token;

        return token ? this.jwtHelper.decodeToken(token) : undefined;
    }

    public get loggedIn(): boolean {
        const token = this.token;

        return token ? !this.jwtHelper.isTokenExpired(token) : false;
    }

    public login(username: string, password: string) {
        return this.apollo.mutate<LoginResponse, { userCredentials: { username: string; password: string; } }>({
            mutation: login,
            variables: {
                userCredentials: {
                    username,
                    password
                }
            }
        }).pipe(
            map(response => {
                const { data: { login } } = response;

                if (login && login.token && login.user) {
                    localStorage.setItem('access_token', login.token);
                    localStorage.setItem('user', JSON.stringify(login.user));
                }

                return login;
            })
        );
    }

    public logout(): void {
        localStorage.removeItem('access_token');
        localStorage.removeItem('user');
    }
}
