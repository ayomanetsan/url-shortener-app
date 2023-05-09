import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RequestUrl } from 'src/app/models/requestUrl';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-redirect',
  templateUrl: './redirect.component.html',
  styleUrls: ['./redirect.component.css']
})
export class RedirectComponent implements OnInit {  

  constructor(
    private activatedRoute: ActivatedRoute,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    const shortUrl = this.activatedRoute.snapshot.paramMap.get('shortUrl');
    const url = `${environment.apiBaseUrl}/url/redirect/${shortUrl}`;

    this.http.get<RequestUrl>(url).subscribe(
      (response) => {
        window.location.href = response.url;
      },
      () => {
        this.router.navigate(['/table']);
      }
    );
  }
}
