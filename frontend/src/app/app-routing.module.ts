import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [];

@NgModule({
  imports: [
    RouterModule.forRoot([
      { path: 'sign-up', component: RegisterComponent },
      { path: 'sign-in', component: LoginComponent },
  ]),
],
  exports: [RouterModule]
})
export class AppRoutingModule { }
