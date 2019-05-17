import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    title = 'Workout Helper';

    private authService: AuthService;


    public constructor(authService: AuthService) {
        this.authService = authService;
    }

    public ngOnInit(): void {
        this.authService.decodeToken();
        this.authService.setUser();
    }
}
