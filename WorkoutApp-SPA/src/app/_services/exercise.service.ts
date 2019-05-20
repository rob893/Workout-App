import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Exercise } from '../_models/exercise';

@Injectable({
    providedIn: 'root'
})
export class ExerciseService {
    private baseUrl: string = environment.apiUrl;
    private http: HttpClient;


    public constructor(http: HttpClient) { 
        this.http = http;
    }

    public getExerciseDetailed(id: number): Observable<Exercise> {
        return this.http.get<Exercise>(this.baseUrl + 'exercises/' + id + '/detailed');
    }
}
