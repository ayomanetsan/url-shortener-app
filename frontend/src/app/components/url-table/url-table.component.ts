import { Component, OnInit } from '@angular/core';
import { UrlService } from 'src/app/core/services/url.service';
import { Url } from 'src/app/models/url';

@Component({
  selector: 'app-url-table',
  templateUrl: './url-table.component.html',
  styleUrls: ['./url-table.component.css']
})
export class UrlTableComponent implements OnInit {
  
  urlList: Url[] = [];

  constructor(private url: UrlService) { }

  ngOnInit() { 
    this.url.GetAll().subscribe(response => this.urlList = response);
  }
}
