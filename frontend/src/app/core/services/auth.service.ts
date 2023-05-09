import { Injectable } from '@angular/core';
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  isAuthenticated(): boolean {
    const decoded: { exp: string } = jwt_decode(localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user') as string).token : null);
    
    return parseInt(decoded.exp) > Date.now() / 1000;
  }
}
