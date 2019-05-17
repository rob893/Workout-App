import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
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

    public getWorkoutPlansForUser(userId: number) {
        return this.http.get(this.baseUrl + 'users/' + userId + '/workoutPlan');
    }
}
