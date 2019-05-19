import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { PaginatedResults } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { Workout } from '../_models/workout';
import { WorkoutParams } from '../_models/queryParams';


@Injectable({
    providedIn: 'root'
})
export class UserService {
    baseUrl: string = environment.apiUrl;

    private http: HttpClient;


    public constructor(http: HttpClient) { 
        this.http = http;
    }

    public getUsers(page?: number, itemsPerPage?: number): Observable<PaginatedResults<User[]>> {
        const paginatedResult: PaginatedResults<User[]> = new PaginatedResults<User[]>();

        let params: HttpParams = new HttpParams();

        if (page != null && itemsPerPage != null) {
            params = params.append('pageNumber', page.toString());
            params = params.append('pageSize', itemsPerPage.toString());
        }
        

        return this.http.get<User[]>(this.baseUrl + 'users', {observe: 'response', params})
            .pipe( //pipe gets us access to the rxjs operators 
                map(response => {
                    paginatedResult.results = response.body;

                    if (response.headers.get('Pagination') != null) {
                        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
                    }

                    return paginatedResult;
                })
            );
    }

    public getUser(id: number): Observable<User> {
        return this.http.get<User>(this.baseUrl + 'users/' + id);
    }

    public updateUser(id: number, user: User): Observable<User> {
        return this.http.put<User>(this.baseUrl + 'users/' + id, user);
    }
    
    public getWorkoutsForUser(userId: number, woParams: WorkoutParams): Observable<PaginatedResults<Workout[]>> {
        let paginatedResult: PaginatedResults<Workout[]> = new PaginatedResults<Workout[]>();
        let params: HttpParams = new HttpParams();

        if (woParams.pageNumber != null && woParams.pageSize != null) {
            params = params.append('pageNumber', woParams.pageNumber.toString());
            params = params.append('pageSize', woParams.pageSize.toString());
        }

        if (woParams.minDate != null) {
            params = params.append('minDate', woParams.minDate.toString());
        }

        if (woParams.maxDate != null) {
            params = params.append('maxDate', woParams.maxDate.toString());
        }


        return this.http.get<Workout[]>(this.baseUrl + 'users/' + userId + '/workouts', {observe: 'response', params}) .pipe( //pipe gets us access to the rxjs operators 
            map(response => {
                paginatedResult.results = response.body;

                if (response.headers.get('Pagination') != null) {
                    paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
                }

                return paginatedResult;
            })
        );;
    }
}
