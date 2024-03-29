import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { NewUser } from '../../models/newUser';
import { User } from '../../models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  
  constructor(private http: HttpClient, private router: Router) {}
  
  regForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('')
  });

  signUp() {
    if (this.regForm.valid) {
      const newUser: NewUser = { ...this.regForm.value } as NewUser;

      this.http.post<User>(`${environment.apiBaseUrl}/auth/register`, newUser).subscribe(
        (response: User) => {
          var userJson = JSON.stringify(response);
          localStorage.setItem('user', userJson);
          this.router.navigate(['/table']);
        },
        (error) => {
          alert('User already exists. Please try logging in instead');
        }
      );
    }
  }
}
