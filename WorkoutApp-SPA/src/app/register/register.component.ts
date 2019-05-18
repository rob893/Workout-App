import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { User } from '../_models/user';
import { Router } from '@angular/router';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    @Output() cancelRegister: EventEmitter<boolean> = new EventEmitter();
    
    public user: User;
    public registerForm: FormGroup; //reactive form

    private authService: AuthService;
    private alertify: AlertifyService;
    private formBuilder: FormBuilder;
    private router: Router;


    public constructor(authService: AuthService, alertify: AlertifyService, formBuilder: FormBuilder, router: Router) { 
        this.authService = authService;
        this.alertify = alertify;
        this.formBuilder = formBuilder;
        this.router = router;
    }

    public ngOnInit(): void {
        this.createRegisterForm();
    }

    private createRegisterForm(): void {
        this.registerForm = this.formBuilder.group({
            username: ['', Validators.required],
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            emailAddress: ['', [Validators.email, Validators.required]],
            password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
            confirmPassword: ['', Validators.required]
        }, {
            validator: this.passwordMatchValidator
        });
    }

    //custom validator
    private passwordMatchValidator(g: FormGroup) {
        return g.get('password').value === g.get('confirmPassword').value ? null : { 'mismatch': true };
    }

    public register(): void {
        if (this.registerForm.valid) {
            this.user = Object.assign({}, this.registerForm.value);

            this.authService.register(this.user).subscribe(() => {
                this.alertify.success('Registration successful');
            }, error => {
                this.alertify.error(error);
            }, () => {
                this.authService.login(this.user).subscribe(() => {
                    this.router.navigate(['/users/' + this.user.id + '/planOverview']);
                });
            });
        }
    }

    public cancel(): void {
        this.cancelRegister.emit(false);
    }
}
