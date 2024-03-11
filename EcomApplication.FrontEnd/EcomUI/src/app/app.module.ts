import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import{HttpClientModule} from '@angular/common/http'
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignUpComponent } from './components/UserSignUp/sign-up/sign-up.component';
import { LoginUIComponent } from './components/UserSignUp/LoginIn/login-ui/login-ui.component';
import { ForgotPassComponent } from './components/UserSignUp/ForgotPass/forgot-pass/forgot-pass.component';
import { PageComponent } from './components/UserSignUp/page/page/page.component';
import { DashboardComponent } from './components/Dashboard/dashboard/dashboard.component';
import { AuthenticateUserComponent } from './components/UserSignUp/ForgotPass/authenticate-user/authenticate-user.component';
import { TwofaloginComponent } from './components/UserSignUp/LoginIn/2FALogin/twofalogin/twofalogin.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  declarations: [
    AppComponent,
    SignUpComponent,
    LoginUIComponent,
    ForgotPassComponent,
    PageComponent,
    DashboardComponent,
    AuthenticateUserComponent,
    TwofaloginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatCardModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
