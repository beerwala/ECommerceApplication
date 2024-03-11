import { Component } from '@angular/core';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'app-login-ui',
  templateUrl: './login-ui.component.html',
  styleUrls: ['./login-ui.component.css']
})
export class LoginUIComponent {
  username: string="";
  password: string="";
  rememberMe: boolean=true;
  email: string="";

  login() {
    // Perform login logic, including 2FA
  }

  //  forgotPassword() {
  // //   this.forgotPasswordModal.show(); // Show the modal using ViewChild
  // }
  sendOTP() {
    // Send OTP to the provided email
  }

  otp: string="";

  submitOTP() {
    // Implement your OTP submission logic here
    console.log('Submitted OTP:', this.otp);

    // Close the OTP popup
  //  document.getElementById('otpPopup').style.display = 'none';
  }
}
