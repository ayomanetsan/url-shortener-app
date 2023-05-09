import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { UrlService } from 'src/app/core/services/url.service';
import { RequestUrl } from 'src/app/models/requestUrl';
import { Url } from 'src/app/models/url';

@Component({
  selector: 'app-url-table',
  templateUrl: './url-table.component.html',
  styleUrls: ['./url-table.component.css']
})
export class UrlTableComponent implements OnInit {
  
  urlList: Url[] = [];

  constructor(private url: UrlService, private auth: AuthService) { }

  ngOnInit() { 
    this.url.GetAll().subscribe(response => this.urlList = response);
  }
  
  isAuthenticated(): boolean {
    return this.auth.isAuthenticated();
  }

  isCreator(email: string): boolean {
    return this.auth.isCreator(email);
  }

  shortenUrl(url: string) {
    const requestUrl: RequestUrl = {
      url: url
    }

    return this.url.Shorten(requestUrl).subscribe(response => this.urlList.push(response));
  }
}
