import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { UserCredentials } from 'src/app/models/userCredentials';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private http: HttpClient, private router: Router) {}
  
  logForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('')
  });
  
  signIn() {
    if (this.logForm.valid) {
      const credentials: UserCredentials = { ...this.logForm.value } as UserCredentials;

      this.http.post<User>(`${environment.apiBaseUrl}/auth/login`, credentials).subscribe(
        (response: User) => {
          var userJson = JSON.stringify(response);
          localStorage.setItem('user', userJson);
          this.router.navigate(['/table']);
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }
}
