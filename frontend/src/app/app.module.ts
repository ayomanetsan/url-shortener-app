import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './components/register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { UrlTableComponent } from './components/url-table/url-table.component';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { RedirectComponent } from './components/redirect/redirect.component';
import { InfoComponent } from './components/info/info.component';
import { AuthGuard } from './core/guards/auth.guard';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CreationModalComponent } from './components/creation-modal/creation-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    UrlTableComponent,
    RedirectComponent,
    InfoComponent,
    CreationModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    AuthGuard,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
