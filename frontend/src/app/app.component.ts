import { Component } from '@angular/core';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  
  constructor(private auth: AuthService) { }

  isAuthenticated(): boolean {
    return this.auth.isAuthenticated();
  }

  logout(): void {
    localStorage.clear();
  }
}
