import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { UrlTableComponent } from './components/url-table/url-table.component';
import { RedirectComponent } from './components/redirect/redirect.component';
import { InfoComponent } from './components/info/info.component';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [];

@NgModule({
  imports: [
    RouterModule.forRoot([
      { path: 'sign-up', component: RegisterComponent },
      { path: 'sign-in', component: LoginComponent },
      { path: 'table', component: UrlTableComponent },
      { path: 'info/:id', component: InfoComponent, canActivate: [AuthGuard] },
      { path: 'sh/:shortUrl', component: RedirectComponent },
  ]),
],
  exports: [RouterModule]
})
export class AppRoutingModule { }
