import { Component, OnInit } from '@angular/core';
import { AuthService } from './auth/auth.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    title = 'WorkoutApp-SPA';
    private readonly authService: AuthService;

    public constructor(authService: AuthService) {
        this.authService = authService;
    }

    public async ngOnInit() {
        this.authService.login('admin', 'password').subscribe(res => {
            console.log(res);
            console.log(this.authService.decodedToken);
        });
    }
}
