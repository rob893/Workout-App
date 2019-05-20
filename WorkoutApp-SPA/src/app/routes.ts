import { Routes } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { PlanOverviewComponent } from './plan-overview/plan-overview.component';
import { PlanOverviewResolver } from './_resolvers/planOverview.resolver';
import { WorkoutDetailsComponent } from './workout-details/workout-details.component';
import { WorkoutDetailsResolver } from './_resolvers/workout-details.resolver';
import { ExerciseDetailsComponent } from './exercise-details/exercise-details.component';
import { ExerciseDetailsResolver } from './_resolvers/exercise-details.resolver';


export const appRoutes: Routes = [
    {
        path: '',
        component:HomeComponent
    },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {
                path: 'planOverview',
                component: PlanOverviewComponent,
                resolve: {workoutPlan: PlanOverviewResolver}
            },
            {
                path: 'workoutDetails',
                component: WorkoutDetailsComponent,
                resolve: {workouts: WorkoutDetailsResolver}
            },
            {
                path: 'exercise/:id',
                component: ExerciseDetailsComponent,
                resolve: {exercise: ExerciseDetailsResolver}
            }
        ]
    },
    {
        path: '**',
        redirectTo: '', 
        pathMatch: 'full'
    }
];