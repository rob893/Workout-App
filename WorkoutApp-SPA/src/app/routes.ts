import { Routes } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { HomeComponent } from './home/home.component';
import { PlanOverviewComponent } from './plan-overview/plan-overview.component';
import { PlanOverviewResolver } from './_resolvers/planOverview.resolver';


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
            }
        ]
    },
    {
        path: '**',
        redirectTo: '', 
        pathMatch: 'full'
    }
];