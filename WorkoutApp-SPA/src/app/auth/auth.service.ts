import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Apollo } from 'apollo-angular';
import { login, registerUser, getUser } from './auth.queries';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, Subject } from 'rxjs';
import { User } from '../shared/models/user.model';
import { TokenClaims } from '../shared/models/token-claims.model';
import { LoginResponse } from './models/login-response.model';
import { UserCredentials } from './models/user-credentials.model';
import { RegisterUser } from './models/register-user.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public authChange = new Subject<boolean>();

  private static readonly accessTokenKey = 'access-token';
  private static readonly refreshTokenKey = 'refresh-token';
  private static readonly userKey = 'user';

  private readonly apollo: Apollo;
  private readonly jwtHelper: JwtHelperService;

  public constructor(apollo: Apollo, jwtHelper: JwtHelperService, private readonly http: HttpClient) {
    this.apollo = apollo;
    this.jwtHelper = jwtHelper;
  }

  public get user(): User | undefined {
    return JSON.parse(localStorage.getItem(AuthService.userKey));
  }

  public get token(): string | undefined {
    return localStorage.getItem(AuthService.accessTokenKey);
  }

  public get decodedToken(): TokenClaims {
    const token = this.token;

    return token ? this.jwtHelper.decodeToken(token) : undefined;
  }

  public get loggedIn(): boolean {
    const token = this.token;

    return token ? !this.jwtHelper.isTokenExpired(token) : false;
  }

  public login(username: string, password: string): Observable<LoginResponse> {
    return this.apollo
      .mutate<{ login: LoginResponse }, { userCredentials: UserCredentials }>({
        mutation: login,
        variables: {
          userCredentials: {
            username,
            password,
            source: 'web'
          }
        }
      })
      .pipe(
        map(response => {
          const {
            data: { login }
          } = response;

          if (login && login.token && login.user) {
            this.authChange.next(true);
            localStorage.setItem(AuthService.accessTokenKey, login.token);
            localStorage.setItem(AuthService.refreshTokenKey, login.refreshToken);
            localStorage.setItem(AuthService.userKey, JSON.stringify(login.user));
          }

          return login;
        })
      );
  }

  public registerUser(userToRegister: RegisterUser): Observable<User> {
    return this.apollo
      .mutate<{ registerUser: { user: User } }, { user: RegisterUser }>({
        mutation: registerUser,
        variables: {
          user: userToRegister
        }
      })
      .pipe(
        map(response => {
          const {
            data: {
              registerUser: { user }
            }
          } = response;
          this.authChange.next(true);

          return user;
        })
      );
  }

  public getUser(userId: number): Observable<any> {
    return this.apollo
      .query<{ user: { firstName: string; lastName: string } }, { userId: number }>({
        query: getUser,
        variables: {
          userId
        }
      })
      .pipe(
        map(response => {
          const {
            data: { user }
          } = response;

          return user;
        })
      );
  }

  public test(): Observable<any> {
    return this.http.get('http://localhost:5002/equipment');
  }

  public logout(): void {
    localStorage.removeItem(AuthService.accessTokenKey);
    localStorage.removeItem(AuthService.userKey);
    this.authChange.next(false);
  }
}
