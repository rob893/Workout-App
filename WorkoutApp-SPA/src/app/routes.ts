import { Routes } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';


export const appRoutes: Routes = [
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: []
    },
    {
        path: '**',
        redirectTo: '', 
        pathMatch: 'full'
    }
];