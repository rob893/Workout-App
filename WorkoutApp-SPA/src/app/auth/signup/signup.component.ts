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

  private readonly authService: AuthService;

  public constructor(authService: AuthService) {
    this.authService = authService;
  }

  public ngOnInit(): void {
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  public onSubmit(form: NgForm): void {
    this.authService
      .registerUser({
        username: form.value.username,
        firstName: form.value.firstName,
        lastName: form.value.lastName,
        email: form.value.email,
        password: form.value.password
      })
      .subscribe(
        response => console.log(response),
        error => console.error(error)
      );
  }
}
