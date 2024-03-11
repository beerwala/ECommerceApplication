import { Component } from '@angular/core';

@Component({
  selector: 'app-twofalogin',
  templateUrl: './twofalogin.component.html',
  styleUrls: ['./twofalogin.component.css']
})
export class TwofaloginComponent {
  otp: string = '';

  constructor() { }

  ngOnInit(): void {
  }
  submitOTP() {
    // Implement OTP verification logic here
    console.log('Submitted OTP:', this.otp);
    // You can add further logic to validate the OTP and proceed accordingly
  }

  closePopup() {
    // Implement close popup logic here, such as hiding the popup
    console.log('Closing popup');
    // You can use Angular Material Dialog or Bootstrap modal to handle popup visibility
  }
}
