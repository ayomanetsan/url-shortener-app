import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UrlService } from 'src/app/core/services/url.service';
import { Url } from 'src/app/models/url';
import * as moment from 'moment';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.css']
})
export class InfoComponent implements OnInit {

  urlInfo: Url = { 
    id: 0,
    shortUrl: '',
    originalUrl: '',
    createdBy: { 
      firstName: '',
      lastName: '',
      email: '',
    },
    createdAt: new Date(),
  };

  date: string = '';

  constructor(private url: UrlService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');

    if (id) {
      this.url.GetById(parseInt(id)).subscribe(response => {
        this.urlInfo = response;

        this.date = moment(response.createdAt).format('MMMM Do YYYY, h:mm:ss a');
      });
    }
  }
}
