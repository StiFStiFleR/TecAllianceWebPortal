import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthenticationService } from '../../_services/authentication.service';
import { User } from '../../_model/user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  submitted: boolean = false;
  loginError: string;
  hasError: boolean = false;


  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private router: Router,
  ) {

  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  get form() { return this.loginForm.controls; }

  submit() {
    this.submitted = true;
    this.hasError = false;
    if (this.loginForm.invalid) {
      this.submitted = false;
      return;
    }

    this.authService.login(new User(this.form.email.value)).subscribe({
      complete: () => {
        this.submitted = false;
        this.router.navigate(['/notes']);
      },
      error: (data) => {
        this.hasError = true;
        this.loginError = data.error;
        this.submitted = false;
      }
    });
  }
}
