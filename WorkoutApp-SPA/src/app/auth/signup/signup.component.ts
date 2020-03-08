import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from '../auth.service';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

    public maxDate: Date;
    

    public constructor(private readonly authService: AuthService) { }

    public ngOnInit(): void {
        this.maxDate = new Date();
        this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
    }

    public onSubmit(form: NgForm): void {
        // this.authService.registerUser({
        //     email: form.value.email,
        //     password: form.value.password
        // });
    }
}
