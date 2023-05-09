import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Url } from 'src/app/models/url';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UrlService {

  constructor(private http: HttpClient) { }

  GetAll(): Observable<Url[]> {
    return this.http.get<Url[]>(`${environment.apiBaseUrl}/url/all`);
  }

  GetById(id: number): Observable<Url> {
    return this.http.get<Url>(`${environment.apiBaseUrl}/url/${id}`);
  }

  Shorten(url: string): Observable<string> {
    return this.http.post<string>(`${environment.apiBaseUrl}/url/shorten`, { url });
  }
}
