import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticateUserComponent } from './components/UserSignUp/ForgotPass/authenticate-user/authenticate-user.component';
import { ForgotPassComponent } from './components/UserSignUp/ForgotPass/forgot-pass/forgot-pass.component';
import { TwofaloginComponent } from './components/UserSignUp/LoginIn/2FALogin/twofalogin/twofalogin.component';
import { LoginUIComponent } from './components/UserSignUp/LoginIn/login-ui/login-ui.component';
import { PageComponent } from './components/UserSignUp/page/page/page.component';
import { SignUpComponent } from './components/UserSignUp/sign-up/sign-up.component';

const routes: Routes = [
  { path: '', component: PageComponent },
  { path: 'Register', component: SignUpComponent },
  { 
    path: 'login', 
    component: LoginUIComponent,
    children: [
      { 
        path: 'forgotPassword', 
        component: ForgotPassComponent,
        children: [
          { path: 'verifyByEmail', component: AuthenticateUserComponent }
        ]
      },
      { path: 'TFAlogin', component: TwofaloginComponent }
    ]
  }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
