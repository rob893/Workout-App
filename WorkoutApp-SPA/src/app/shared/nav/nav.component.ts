import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.scss']
})
export class NavComponent {

    private readonly authService: AuthService;


    public constructor(authService: AuthService) { 
        this.authService = authService;
    }

    public get loggedIn(): boolean {
        return this.authService.loggedIn;
    }
}
