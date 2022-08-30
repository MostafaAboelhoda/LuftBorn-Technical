import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form!: FormGroup;
  loading = false;
  submitted = false;

  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private accountService: AuthService
  ) { }

  ngOnInit() {
      this.form = this.formBuilder.group({
          email: ['', [Validators.required,Validators.email]],
          password: ['', Validators.required]
      });
  }

  get f() { return this.form.controls; }

  
  onSubmit() {
    this.submitted = true;


    // stop here if form is invalid
    if (this.form.invalid) {
        return;
    }

    this.loading = true;
    this.accountService.login(this.form.get("email")?.value, this.form.get("password")?.value)
        .subscribe({
            next: () => {
           
                this.router.navigateByUrl("employees");
            },
            error: error => {
                // this.alertService.error(error);
                this.loading = false;
            }
        });
}
}
