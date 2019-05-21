import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Workout } from '../_models/workout';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class WorkoutPlanService {

    private baseUrl: string = environment.apiUrl;
    private http: HttpClient;


    public constructor(http: HttpClient) { 
        this.http = http;
    }

    // public completeWorkout(woId: number): Observable<Workout> {
    //     return this.http.patch(this.baseUrl + '')
    // }
}
