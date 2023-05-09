import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { catchError, of } from 'rxjs';
import { UrlService } from 'src/app/core/services/url.service';

@Component({
  selector: 'app-creation-modal',
  templateUrl: './creation-modal.component.html',
  styleUrls: ['./creation-modal.component.css']
})
export class CreationModalComponent implements OnInit {

  urlForm: FormGroup = new FormGroup({});

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<CreationModalComponent>,
    private url: UrlService
  ) { }

  ngOnInit() {
    this.urlForm = this.formBuilder.group({
      ['url']: ['', [Validators.required, Validators.pattern('https?://.+')]]
    });
  }

  createUrl() {
    this.url.Shorten(this.urlForm.value).pipe(
      catchError(() => {
        alert('This URL already exists!');
        return of(null);
      })
    ).subscribe(() => {
        this.dialogRef.close();
    });
  }
}
