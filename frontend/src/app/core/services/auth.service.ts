import { Injectable } from '@angular/core';
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  isAuthenticated(): boolean {
    const userString = localStorage.getItem('user');
    const user = userString ? JSON.parse(userString) : null;
    const decoded: { exp: string } | null = user ? jwt_decode(user.token) : null;

    if (decoded) {
      return parseInt(decoded.exp) > Date.now() / 1000;
    }

    return false;
  }

  isCreator(email: string) {
    const userString = localStorage.getItem('user');
    const user = userString ? JSON.parse(userString) : null;
    const decoded: { unique_name: string } | null = user ? jwt_decode(user.token) : null;

    if (decoded) {
      return decoded.unique_name == email;
    }

    return false;
  }

  isAdmin(): boolean { 
    const userString = localStorage.getItem('user');
    const user = userString ? JSON.parse(userString) : null;
    const decoded: { role: string } | null = user ? jwt_decode(user.token) : null;

    if (decoded) {
      return decoded.role == 'Admin';
    }

    return false;
  }
}
