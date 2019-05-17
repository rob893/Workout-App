import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from  'rxjs/operators';
import { Observable, BehaviorSubject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';


@Injectable({
    providedIn: 'root'
})
export class AuthService {

    public baseUrl: string = environment.apiUrl + 'auth/';
    public decodedToken: any;
    public currentUser: User;
    
    private http: HttpClient;
    private jwtHelper = new JwtHelperService();
    

    public constructor(http: HttpClient) {
        this.http = http;
    }

    public login(model: any): Observable<void> {
        return this.http.post(this.baseUrl + 'login', model).pipe(
            map((response: any) => {
                const user = response;

                if (user) {
                    localStorage.setItem('token', user.token);
                    localStorage.setItem('user', JSON.stringify(user.user));
                    this.decodedToken = this.jwtHelper.decodeToken(user.token);
                    this.currentUser = user.user;
                }
            })
        );
    }

    public register(user: User): Observable<Object> {
        return this.http.post(this.baseUrl + 'register', user);
    }

    public loggedIn(): boolean {
        const token = localStorage.getItem('token');

        return !this.jwtHelper.isTokenExpired(token);
    }

    public decodeToken(): void {
        const token = localStorage.getItem('token');

        if (token) {
            this.decodedToken = this.jwtHelper.decodeToken(token);
        }
    }

    public setUser(): void {
        const user: User = JSON.parse(localStorage.getItem('user'));

        if (user) {
            this.currentUser = user;
        }
    }
}
