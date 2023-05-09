import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestUrl } from 'src/app/models/requestUrl';
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

  Shorten(url: RequestUrl): Observable<Url> {
    return this.http.post<Url>(`${environment.apiBaseUrl}/url/shorten`, url);
  }

  Delete(url: RequestUrl): Observable<Url[]> {
    return this.http.delete<Url[]>(`${environment.apiBaseUrl}/url/delete?shortUrl=${url.url}`);
  }
}
