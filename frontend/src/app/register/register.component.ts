import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  
  constructor(private http: HttpClient) {}
  
  regForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('')
  });

  signUp() {
    const formData = this.regForm.value;

    console.log(formData);

    this.http.post(`${environment.apiBaseUrl}/auth/register`, formData).subscribe(
      (response) => {
        console.log('API call successful:', response);
      },
      (error) => {
        console.log('API call error:', error);
      }
    );
  }
}
