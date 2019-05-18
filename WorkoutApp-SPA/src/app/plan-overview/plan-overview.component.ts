import { Component, OnInit } from '@angular/core';
import { WorkoutPlanService } from '../_services/workoutPlan.service';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { WorkoutPlan } from '../_models/workoutPlan';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-plan-overview',
    templateUrl: './plan-overview.component.html',
    styleUrls: ['./plan-overview.component.css']
})
export class PlanOverviewComponent implements OnInit {
    public workoutPlan: WorkoutPlan;
    
    private woPlanService: WorkoutPlanService;
    private authService: AuthService;
    private alertify: AlertifyService;
    private route: ActivatedRoute;
    

    public constructor(woPlanService: WorkoutPlanService, authService: AuthService, alertify: AlertifyService, route: ActivatedRoute) { 
        this.woPlanService = woPlanService;
        this.authService = authService;
        this.alertify = alertify;
        this.route = route;
    }

    public ngOnInit() {
        this.route.data.subscribe(data => {
            this.workoutPlan = data['workoutPlan'][0];
        })
    }
}
