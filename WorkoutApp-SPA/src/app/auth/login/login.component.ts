import { Component, OnInit } from '@angular/core';
import { NgForm, FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public loginForm: FormGroup;

  private readonly authService: AuthService;

  public constructor(authService: AuthService) {
    this.authService = authService;
  }

  public ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl('', {
        validators: [Validators.required]
      }),
      password: new FormControl('', { validators: [Validators.required] })
    });
  }

  public onSubmit(): void {
    this.authService.login(this.loginForm.value.username, this.loginForm.value.password).subscribe(
      successRes => {
        console.log(successRes);
      },
      error => {
        console.log(error);
      }
    );
  }
}
