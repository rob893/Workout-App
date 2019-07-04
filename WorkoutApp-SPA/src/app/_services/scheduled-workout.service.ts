import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { ScheduledWorkout } from '../_models/scheduledWorkout';

@Injectable({
  providedIn: 'root'
})
export class ScheduledWorkoutService {

    private baseUrl: string = environment.apiUrl;
    private http: HttpClient;


    public constructor(http: HttpClient) { 
        this.http = http;
    }

    public startScheduledWorkout(id: number): Observable<ScheduledWorkout> {
        return this.http.patch<ScheduledWorkout>(this.baseUrl + 'scheduledWorkouts/' + id + '/startWorkout', []);
    }
}
