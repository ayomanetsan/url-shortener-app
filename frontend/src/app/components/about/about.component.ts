import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { UrlService } from 'src/app/core/services/url.service';
import { Description } from 'src/app/models/description';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  description: string = '';

  constructor(private url: UrlService, private auth: AuthService,) { }

  ngOnInit() { 
    this.url.GetDescription().subscribe(response => this.description = response.content);
  }

  isAdmin(): boolean {
    return this.auth.isAdmin();
  }

  setDescription() {
    const content: Description = { content: this.description };

    this.url.SetDescription(content).subscribe(response => this.description = response.content);
  }
}
