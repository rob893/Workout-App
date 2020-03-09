import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';

@Component({
    selector: 'app-welcome',
    templateUrl: './welcome.component.html',
    styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {

    constructor(private readonly authService: AuthService) { }

    ngOnInit(): void {
    }

    test() {
        this.authService.getUser(3).subscribe(res => console.log(res));
    }
}
